using System;

namespace Iban
{
    /// <summary>
    /// IbanValidation result.
    /// </summary>
    public class IbanValidationResult : IComparable<IbanValidationResult>
    {
        /// <summary>
        /// True if success
        /// </summary>
        public bool Success { set; get; } = false;

        public string Iban { get; }

        /// <summary>
        /// Description if needed
        /// </summary>
        public string Message { set; get; }

        public IbanValidationResult(string iban)
        {
            Iban = iban;
        }

        public IbanValidationResult(string iban, string message)
        {
            Iban = iban;
            Message = message;
        }

        public int CompareTo(IbanValidationResult other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var successComparison = Success.CompareTo(other.Success);
            if (successComparison != 0) return successComparison;
            return string.Compare(Message, other.Message, StringComparison.Ordinal);
        }
    }
}
