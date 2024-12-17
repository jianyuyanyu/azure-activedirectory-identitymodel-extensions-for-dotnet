// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Constants for JsonWebAlgorithms  "kty" Key Type (sec 6.1)
    /// https://datatracker.ietf.org/doc/html/rfc7518#section-6.1
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0036:Annotate nullability of public types and members in the declared API", Justification = "Nullability annotations not yet added.")]
    public static class JsonWebAlgorithmsKeyTypes
    {
#pragma warning disable 1591
        public const string EllipticCurve = "EC";
        public const string RSA = "RSA";
        public const string Octet = "oct";
#pragma warning restore 1591
    }
}
