// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#pragma warning disable 1591

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Constants for compression algorithms.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0036:Annotate nullability of public types and members in the declared API", Justification = "Nullability annotations not yet added.")]
    public class CompressionAlgorithms
    {
        /// <summary>
        /// See: <see href="https://datatracker.ietf.org/doc/html/rfc1951"/>.
        /// </summary>
        public const string Deflate = "DEF";
    }
}

#pragma warning restore 1591
