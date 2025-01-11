// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.IdentityModel.Validators
{
    internal static class AadTokenValidationParametersExtensionBase
    {
        /// <summary>
        /// Validates the issuer signing key.
        /// </summary>
        /// <param name="securityKey">The <see cref="SecurityKey"/> that signed the <see cref="SecurityToken"/>.</param>
        /// <param name="securityToken">The <see cref="SecurityToken"/> being validated, could be a JwtSecurityToken or JsonWebToken.</param>
        /// <param name="configuration">The <see cref="BaseConfiguration"/> provided.</param>
        /// <returns><c>true</c> if the issuer of the signing key is valid; otherwise, <c>false</c>.</returns>
        internal static bool ValidateIssuerSigningKey(SecurityKey securityKey, SecurityToken securityToken, BaseConfiguration configuration)
        {
            if (securityKey == null)
                return true;

            if (securityToken == null)
                throw LogHelper.LogArgumentNullException(nameof(securityToken));

            if (configuration is not OpenIdConnectConfiguration openIdConnectConfiguration)
                return true;

            JsonWebKey matchedKeyFromConfig = GetJsonWebKeyBySecurityKey(openIdConnectConfiguration, securityKey);
            if (matchedKeyFromConfig != null && matchedKeyFromConfig.AdditionalData.TryGetValue(OpenIdProviderMetadataNames.Issuer, out object value))
            {
                string signingKeyIssuer = value as string;
                if (string.IsNullOrWhiteSpace(signingKeyIssuer))
                    return true;

                string tenantIdFromToken = GetTid(securityToken);
                if (string.IsNullOrEmpty(tenantIdFromToken))
                {
                    if (AppContextSwitches.DontFailOnMissingTid)
                        return true;

                    throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogMessages.IDX40009));
                }

                string tokenIssuer = securityToken.Issuer;

#if NET6_0_OR_GREATER
                if (!string.IsNullOrEmpty(tokenIssuer) && !tokenIssuer.Contains(tenantIdFromToken, StringComparison.Ordinal))
                    throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogHelper.FormatInvariant(LogMessages.IDX40004, LogHelper.MarkAsNonPII(tokenIssuer), LogHelper.MarkAsNonPII(tenantIdFromToken))));

                // creating an effectiveSigningKeyIssuer is required as signingKeyIssuer might contain {tenantid}
                string effectiveSigningKeyIssuer = signingKeyIssuer.Replace(AadIssuerValidator.TenantIdTemplate, tenantIdFromToken, StringComparison.Ordinal);
                string v2TokenIssuer = openIdConnectConfiguration.Issuer?.Replace(AadIssuerValidator.TenantIdTemplate, tenantIdFromToken, StringComparison.Ordinal);
#else
                if (!string.IsNullOrEmpty(tokenIssuer) && !tokenIssuer.Contains(tenantIdFromToken))
                    throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogHelper.FormatInvariant(LogMessages.IDX40004, LogHelper.MarkAsNonPII(tokenIssuer), LogHelper.MarkAsNonPII(tenantIdFromToken))));

                // creating an effectiveSigningKeyIssuer is required as signingKeyIssuer might contain {tenantid}
                string effectiveSigningKeyIssuer = signingKeyIssuer.Replace(AadIssuerValidator.TenantIdTemplate, tenantIdFromToken);
                string v2TokenIssuer = openIdConnectConfiguration.Issuer?.Replace(AadIssuerValidator.TenantIdTemplate, tenantIdFromToken);
#endif

                // comparing effectiveSigningKeyIssuer with v2TokenIssuer is required as well because of the following scenario:
                // 1. service trusts /common/v2.0 endpoint 
                // 2. service receives a v1 token that has issuer like sts.windows.net
                // 3. signing key issuers will never match sts.windows.net as v1 endpoint doesn't have issuers attached to keys
                // v2TokenIssuer is the representation of Token.Issuer (if it was a v2 issuer)
                if (effectiveSigningKeyIssuer != tokenIssuer && effectiveSigningKeyIssuer != v2TokenIssuer)
                    throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogHelper.FormatInvariant(LogMessages.IDX40005, LogHelper.MarkAsNonPII(tokenIssuer), LogHelper.MarkAsNonPII(effectiveSigningKeyIssuer))));
            }

            return true;
        }

        private static void EnforceSingleClaimCaseInsensitive(IEnumerable<string> keys, string claimType)
        {
            bool claimSeen = false;
            foreach (var key in keys)
            {
                if (string.Equals(key, claimType, StringComparison.OrdinalIgnoreCase))
                {
                    if (claimSeen)
                        throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogHelper.FormatInvariant(LogMessages.IDX40011, claimType)));

                    claimSeen = true;
                }
            }
        }

        internal static JsonWebKey GetJsonWebKeyBySecurityKey(OpenIdConnectConfiguration configuration, SecurityKey securityKey)
        {
            if (configuration.JsonWebKeySet == null)
                return null;

            foreach (JsonWebKey key in configuration.JsonWebKeySet.Keys)
            {
                if (key.Kid == securityKey.KeyId)
                    return key;
            }

            return null;
        }

        private static string GetTid(SecurityToken securityToken)
        {
            switch (securityToken)
            {
                case JsonWebToken jsonWebToken:
                    if (jsonWebToken.TryGetPayloadValue<string>(AadIssuerValidatorConstants.Tid, out string tid))
                    {
                        EnforceSingleClaimCaseInsensitive(jsonWebToken.PayloadClaimNames, AadIssuerValidatorConstants.Tid);
                        return tid;
                    }

                    return string.Empty;

                case JwtSecurityToken jwtSecurityToken:
                    if ((jwtSecurityToken.Payload.TryGetValue(AadIssuerValidatorConstants.Tid, out object tidObject) && tidObject is string jwtTid))
                    {
                        EnforceSingleClaimCaseInsensitive(jwtSecurityToken.Payload.Keys, AadIssuerValidatorConstants.Tid);
                        return jwtTid;
                    }

                    return string.Empty;

                default:
                    throw LogHelper.LogExceptionMessage(new SecurityTokenInvalidIssuerException(LogMessages.IDX40010));
            }
        }
    }
}
