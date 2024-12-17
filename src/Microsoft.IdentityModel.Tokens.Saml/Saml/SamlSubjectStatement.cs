// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using static Microsoft.IdentityModel.Logging.LogHelper;

namespace Microsoft.IdentityModel.Tokens.Saml
{
    /// <summary>
    /// Represents the SubjectStatement element.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ApiDesign", "RS0036:Annotate nullability of public types and members in the declared API", Justification = "Nullability annotations not yet added.")]
    public abstract class SamlSubjectStatement : SamlStatement
    {
        private SamlSubject _subject;

        /// <summary>
        /// Gets or sets the subject of the statement.
        /// </summary>
        public virtual SamlSubject Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value ?? throw LogArgumentNullException(nameof(value));
            }
        }
    }
}
