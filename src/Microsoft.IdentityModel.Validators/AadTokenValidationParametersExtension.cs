// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Validators
{
    /// <summary>
    /// A generic class for additional validation checks on <see cref="SecurityToken"/> issued by the Microsoft identity platform (AAD).
    /// </summary>
    public static class AadTokenValidationParametersExtension
    {
        /// <summary>
        /// Enables validation of the cloud instance of the Microsoft Entra ID token signing keys.
        /// </summary>
        /// <param name="tokenValidationParameters">The <see cref="TokenValidationParameters"/> that are used to validate the token.</param>
        public static void EnableEntraIdSigningKeyCloudInstanceValidation(this TokenValidationParameters tokenValidationParameters)
        {
            if (tokenValidationParameters == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenValidationParameters));

            IssuerSigningKeyValidatorUsingConfiguration userProvidedIssuerSigningKeyValidatorUsingConfiguration = tokenValidationParameters.IssuerSigningKeyValidatorUsingConfiguration;
            IssuerSigningKeyValidator userProvidedIssuerSigningKeyValidator = tokenValidationParameters.IssuerSigningKeyValidator;

            tokenValidationParameters.IssuerSigningKeyValidatorUsingConfiguration = (securityKey, securityToken, tvp, config) =>
            {
                ValidateSigningKeyCloudInstance(securityKey, config);

                // preserve and run provided logic
                if (userProvidedIssuerSigningKeyValidatorUsingConfiguration != null)
                    return userProvidedIssuerSigningKeyValidatorUsingConfiguration(securityKey, securityToken, tvp, config);

                if (userProvidedIssuerSigningKeyValidator != null)
                    return userProvidedIssuerSigningKeyValidator(securityKey, securityToken, tvp);

                return true;
            };
        }

        /// <summary>
        /// Enables the validation of the issuer of the signing keys used by the Microsoft identity platform (AAD) against the issuer of the token.
        /// </summary>
        /// <param name="tokenValidationParameters">The <see cref="TokenValidationParameters"/> that are used to validate the token.</param>
        public static void EnableAadSigningKeyIssuerValidation(this TokenValidationParameters tokenValidationParameters)
        {
            if (tokenValidationParameters == null)
                throw LogHelper.LogArgumentNullException(nameof(tokenValidationParameters));

            IssuerSigningKeyValidatorUsingConfiguration userProvidedIssuerSigningKeyValidatorUsingConfiguration = tokenValidationParameters.IssuerSigningKeyValidatorUsingConfiguration;
            IssuerSigningKeyValidator userProvidedIssuerSigningKeyValidator = tokenValidationParameters.IssuerSigningKeyValidator;

            tokenValidationParameters.IssuerSigningKeyValidatorUsingConfiguration = (securityKey, securityToken, tvp, config) =>
            {
                AadTokenValidationParametersExtensionBase.ValidateIssuerSigningKey(securityKey, securityToken, config);

                // preserve and run provided logic
                if (userProvidedIssuerSigningKeyValidatorUsingConfiguration != null)
                    return userProvidedIssuerSigningKeyValidatorUsingConfiguration(securityKey, securityToken, tvp, config);

                if (userProvidedIssuerSigningKeyValidator != null)
                    return userProvidedIssuerSigningKeyValidator(securityKey, securityToken, tvp);

                return ValidateIssuerSigningKeyCertificate(securityKey, tvp);
            };
        }
        /// <summary>
        /// Validates the cloud instance of the signing key.
        /// </summary>
        /// <param name="securityKey">The <see cref="SecurityKey"/> that signed the <see cref="SecurityToken"/>.</param>
        /// <param name="configuration">The <see cref="BaseConfiguration"/> provided.</param>
        internal static void ValidateSigningKeyCloudInstance(SecurityKey securityKey, BaseConfiguration configuration)
        {
            if (securityKey == null)
                return;

            if (configuration is not OpenIdConnectConfiguration openIdConnectConfiguration)
                return;

            JsonWebKey matchedKeyFromConfig = AadTokenValidationParametersExtensionBase.GetJsonWebKeyBySecurityKey(openIdConnectConfiguration, securityKey);
            if (matchedKeyFromConfig != null && matchedKeyFromConfig.AdditionalData.TryGetValue(AadIssuerValidatorConstants.CloudInstanceNameKey, out object value))
            {
                string signingKeyCloudInstanceName = value as string;
                if (string.IsNullOrWhiteSpace(signingKeyCloudInstanceName))
                    return;

                if (openIdConnectConfiguration.AdditionalData.TryGetValue(AadIssuerValidatorConstants.CloudInstanceNameKey, out object configurationCloudInstanceNameObjectValue))
                {
                    string configurationCloudInstanceName = configurationCloudInstanceNameObjectValue as string;
                    if (string.IsNullOrWhiteSpace(configurationCloudInstanceName))
                        return;

                    if (!string.Equals(signingKeyCloudInstanceName, configurationCloudInstanceName, StringComparison.Ordinal))
                        throw LogHelper.LogExceptionMessage(
                            new SecurityTokenInvalidCloudInstanceException(LogHelper.FormatInvariant(LogMessages.IDX40012, LogHelper.MarkAsNonPII(signingKeyCloudInstanceName), LogHelper.MarkAsNonPII(configurationCloudInstanceName)))
                            {
                                ConfigurationCloudInstanceName = configurationCloudInstanceName,
                                SigningKeyCloudInstanceName = signingKeyCloudInstanceName,
                                SigningKey = securityKey,
                            });
                }
            }
        }

        /// <summary>
        /// Validates the issuer signing key certificate.
        /// </summary>
        /// <param name="securityKey">The <see cref="SecurityKey"/> that signed the <see cref="SecurityToken"/>.</param>
        /// <param name="validationParameters">The <see cref="TokenValidationParameters"/> that are used to validate the token.</param>
        /// <returns><c>true</c> if the issuer signing key certificate is valid; otherwise, <c>false</c>.</returns>
        internal static bool ValidateIssuerSigningKeyCertificate(SecurityKey securityKey, TokenValidationParameters validationParameters)
        {
            if (!validationParameters.RequireSignedTokens && securityKey == null)
            {
                LogHelper.LogInformation(Tokens.LogMessages.IDX10252);
                return true;
            }
            else if (securityKey == null)
            {
                throw LogHelper.LogExceptionMessage(new ArgumentNullException(nameof(securityKey), LogMessages.IDX40007));
            }

            if (!validationParameters.ValidateIssuerSigningKey)
            {
                LogHelper.LogVerbose(Tokens.LogMessages.IDX10237);
                return true;
            }

            Tokens.Validators.ValidateIssuerSigningKeyLifeTime(securityKey, validationParameters);

            return true;
        }
    }
}
