// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a validated signing key lifetime.
    /// </summary>
    internal readonly struct ValidatedSigningKeyLifetime
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ValidatedSigningKeyLifetime"/>.
        /// </summary>
        /// <param name="ValidFrom">The date from which the signing key is considered valid.</param>
        /// <param name="ValidTo">The date until which the signing key is considered valid.</param>
        /// <param name="ValidationTime">The time the validation occurred.</param>
        internal ValidatedSigningKeyLifetime(DateTime? ValidFrom, DateTime? ValidTo, DateTime? ValidationTime)
        {
            this.ValidFrom = ValidFrom;
            this.ValidTo = ValidTo;
            this.ValidationTime = ValidationTime;
        }

        /// <summary>
        /// The date from which the signing key is considered valid.
        /// </summary>
        public DateTime? ValidFrom { get; }

        /// <summary>
        /// The date until which the signing key is considered valid.
        /// </summary>
        public DateTime? ValidTo { get; }

        /// <summary>
        /// The time the validation occurred.
        /// </summary>
        public DateTime? ValidationTime { get; }
    }
}
#nullable restore
