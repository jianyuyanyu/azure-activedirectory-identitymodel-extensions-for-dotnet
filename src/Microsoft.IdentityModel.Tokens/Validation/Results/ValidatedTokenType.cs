// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a validated token type, including the number of valid types present in the validation parameters.
    /// </summary>
    internal readonly struct ValidatedTokenType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidatedTokenType"/>.
        /// </summary>
        /// <param name="Type">The token type that was validated.</param>
        /// <param name="ValidTypeCount">The number of valid types present in the validation parameters.</param>
        internal ValidatedTokenType(string Type, int ValidTypeCount)
        {
            this.Type = Type;
            this.ValidTypeCount = ValidTypeCount;
        }

        /// <summary>
        /// The token type that was validated.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// The number of valid types present in the validation parameters.
        /// </summary>
        public int ValidTypeCount { get; }
    }
}
#nullable restore
