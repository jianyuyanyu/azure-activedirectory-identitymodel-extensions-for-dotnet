// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Security.Claims;

namespace Microsoft.IdentityModel.Tokens.Saml2
{
    /// <summary>
    /// Defines the keys for properties contained in <see cref="Claim.Properties"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0036:Annotate nullability of public types and members in the declared API", Justification = "Nullability annotations not yet added.")]
    public static class ClaimProperties
    {
#pragma warning disable 1591
        public const string Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties";

        public const string SamlAttributeFriendlyName = Namespace + "/friendlyname";
        public const string SamlAttributeNameFormat = Namespace + "/attributename";
        public const string SamlNameIdentifierFormat = Namespace + "/format";
        public const string SamlNameIdentifierNameQualifier = Namespace + "/namequalifier";
        public const string SamlNameIdentifierSPNameQualifier = Namespace + "/spnamequalifier";
        public const string SamlNameIdentifierSPProvidedId = Namespace + "/spprovidedid";
#pragma warning restore 1591
    }
}
