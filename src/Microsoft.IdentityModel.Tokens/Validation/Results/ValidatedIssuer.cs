// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a validated issuer, including the source of the validation.
    /// </summary>
    internal readonly struct ValidatedIssuer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidatedIssuer"/>.
        /// </summary>
        /// <param name="Issuer">The issuer that was validated.</param>
        /// <param name="ValidationSource">The source of the validation, i.e. configuration, validation parameters.</param>
        public ValidatedIssuer(string Issuer, IssuerValidationSource ValidationSource)
        {
            this.Issuer = Issuer;
            this.ValidationSource = ValidationSource;
        }

        public string Issuer { get; }
        public IssuerValidationSource ValidationSource { get; }
    }
}
#nullable restore
