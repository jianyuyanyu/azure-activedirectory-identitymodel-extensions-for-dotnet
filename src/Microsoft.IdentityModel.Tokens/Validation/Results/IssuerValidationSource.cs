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
        protected IssuerValidationSource(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static readonly IssuerValidationSource NotValidated = new("NotValidated");
        public static readonly IssuerValidationSource IssuerMatchedConfiguration = new("IssuerMatchedConfiguration");
        public static readonly IssuerValidationSource IssuerMatchedValidationParameters = new("IssuerMatchedValidationParameters");
    }
}
#nullable restore
