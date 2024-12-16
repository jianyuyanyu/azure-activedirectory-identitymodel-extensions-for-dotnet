// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents the source of the validation of an issuer.
    /// </summary>
    internal class IssuerValidationSource
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IssuerValidationSource"/>.
        /// </summary>
        /// <param name="name">The name of the issuer validation source.</param>
        public IssuerValidationSource(string name) => Name = name;

        /// <summary>
        /// The name of the issuer validation source.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Represents the issuer validation source that has not been validated.
        /// </summary>
        public static readonly IssuerValidationSource NotValidated = new("NotValidated");

        /// <summary>
        /// Represents the issuer validation source that has been matched with the configuration provided.
        /// </summary>
        public static readonly IssuerValidationSource IssuerMatchedConfiguration = new("IssuerMatchedConfiguration");

        /// <summary>
        /// Represents the issuer validation source that has been matched with the validation parameters provided.
        /// </summary>
        public static readonly IssuerValidationSource IssuerMatchedValidationParameters = new("IssuerMatchedValidationParameters");
    }
}
#nullable restore
