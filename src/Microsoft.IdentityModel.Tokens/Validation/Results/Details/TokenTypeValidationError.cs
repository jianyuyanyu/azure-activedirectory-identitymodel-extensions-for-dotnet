// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;

#nullable enable
namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Represents a token type validation error.
    /// </summary>
    public class TokenTypeValidationError : ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenTypeValidationError"/> class.
        /// </summary>
        /// <param name="messageDetail" /> contains information about the exception that is used to generate the exception message.
        /// <param name="validationFailureType"/> is the type of validation failure that occurred.
        /// <param name="exceptionType"/> is the type of exception that occurred.
        /// <param name="stackFrame"/> is the stack frame where the exception occurred.
        /// <param name="invalidTokenType"/> is the token type that could not be validated.
        /// <param name="innerException"/> is the inner exception that occurred.
        public TokenTypeValidationError(
            MessageDetail messageDetail,
            ValidationFailureType validationFailureType,
            Type exceptionType,
            StackFrame stackFrame,
            string? invalidTokenType,
            Exception? innerException = null)
            : base(messageDetail, validationFailureType, exceptionType, stackFrame, innerException)
        {
            InvalidTokenType = invalidTokenType;
        }

        /// <summary>
        /// Creates an instance of an <see cref="Exception"/> using <see cref="ValidationError"/>
        /// </summary>
        /// <returns>An instance of an exception.</returns>
        public override Exception GetException()
        {
            if (ExceptionType == typeof(SecurityTokenInvalidTypeException))
            {
                SecurityTokenInvalidTypeException exception = new(MessageDetail.Message, InnerException)
                {
                    InvalidType = InvalidTokenType
                };
                exception.SetValidationError(this);

                return exception;
            }

            return base.GetException();
        }

        /// <summary>
        /// Creates a new instance of <see cref="TokenTypeValidationError"/> representing a null parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="stackFrame">The stack frame where the error occurred.</param>
        /// <returns>A new <see cref="TokenTypeValidationError"/>.</returns>
        public static new TokenTypeValidationError NullParameter(string parameterName, StackFrame stackFrame) => new(
            MessageDetail.NullParameter(parameterName),
            ValidationFailureType.NullArgument,
            typeof(SecurityTokenArgumentNullException),
            stackFrame,
            null); // invalidTokenType

        /// <summary>
        /// The token type that could not be validated.
        /// </summary>
        public string? InvalidTokenType { get; }
    }
}
#nullable restore
