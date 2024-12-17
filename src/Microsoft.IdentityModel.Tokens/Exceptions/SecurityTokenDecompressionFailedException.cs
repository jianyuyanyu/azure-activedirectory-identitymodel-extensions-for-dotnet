// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Thrown when JWE decompression fails.
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0036:Annotate nullability of public types and members in the declared API", Justification = "Nullability annotations not yet added.")]
    public class SecurityTokenDecompressionFailedException : SecurityTokenException
    {
        /// <summary>
        /// Initializes a new instance of  <see cref="SecurityTokenDecompressionFailedException"/>
        /// </summary>
        public SecurityTokenDecompressionFailedException()
            : base("SecurityToken decompression failed.")
        {
        }

        /// <summary>
        /// Initializes a new instance of  <see cref="SecurityTokenDecompressionFailedException"/>
        /// </summary>
        public SecurityTokenDecompressionFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of  <see cref="SecurityTokenDecompressionFailedException"/>
        /// </summary>
        public SecurityTokenDecompressionFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTokenDecompressionFailedException"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected SecurityTokenDecompressionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
