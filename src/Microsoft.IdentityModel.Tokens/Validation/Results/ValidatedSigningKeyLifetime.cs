// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a validated signing key lifetime.
    /// </summary>
    public readonly struct ValidatedSigningKeyLifetime : IEquatable<ValidatedSigningKeyLifetime>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current instance of <see cref="ValidatedSigningKeyLifetime"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is ValidatedSigningKeyLifetime other)
            {
                return Equals(other);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="ValidatedSigningKeyLifetime"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ValidFrom.GetHashCode() ^ ValidTo.GetHashCode() ^ ValidationTime.GetHashCode();
        }

        /// <summary>
        /// Equality comparison operator for <see cref="ValidatedSigningKeyLifetime"/>.
        /// </summary>
        /// <param name="left">The left value to compare.</param>
        /// <param name="right">The right value to compare.</param>
        /// <returns>A boolean indicating whether the left value is equal to the right one.</returns>
        public static bool operator ==(ValidatedSigningKeyLifetime left, ValidatedSigningKeyLifetime right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Inequality comparison operator for <see cref="ValidatedSigningKeyLifetime"/>.
        /// </summary>
        /// <param name="left">The left value to compare.</param>
        /// <param name="right">The right value to compare.</param>
        /// <returns>A boolean indicating whether the left value is not equal to the right one.</returns>
        public static bool operator !=(ValidatedSigningKeyLifetime left, ValidatedSigningKeyLifetime right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ValidatedSigningKeyLifetime"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="ValidatedSigningKeyLifetime"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="ValidatedSigningKeyLifetime"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
        public bool Equals(ValidatedSigningKeyLifetime other)
        {
            if (other.ValidFrom != ValidFrom || other.ValidTo != ValidTo || other.ValidationTime != ValidationTime)
            {
                return false;
            }

            return true;
        }
    }
}
#nullable restore
