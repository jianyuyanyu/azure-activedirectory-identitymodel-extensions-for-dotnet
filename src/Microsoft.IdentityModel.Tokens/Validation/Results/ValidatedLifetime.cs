// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a validated lifetime, including the NotBefore and Expires values.
    /// </summary>
    internal readonly struct ValidatedLifetime
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidatedLifetime"/>.
        /// </summary>
        /// <param name="NotBefore">The <see cref="DateTime"/> representing the time from which the token is considered valid.</param>
        /// <param name="Expires">The <see cref="DateTime"/> representing the token's expiration time.</param>
        public ValidatedLifetime(DateTime? NotBefore, DateTime? Expires)
        {
            this.NotBefore = NotBefore;
            this.Expires = Expires;
        }

        /// <summary>
        /// The <see cref="DateTime"/> representing the time from which the token is considered valid.
        /// </summary>
        public DateTime? NotBefore { get; }

        /// <summary>
        /// The <see cref="DateTime"/> representing the token's expiration time.
        /// </summary>
        public DateTime? Expires { get; }
    }
}
#nullable restore
