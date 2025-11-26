using System.Text.RegularExpressions;

namespace FSI.Ecommerce.Domain.ValueObjects
{
    public sealed class Email : IEquatable<Email>
    {
        private static readonly Regex _regex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email must be provided.", nameof(value));

            if (!_regex.IsMatch(value))
                throw new ArgumentException("Invalid e-mail format.", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;

        public bool Equals(Email? other) =>
            other is not null &&
            string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj) => Equals(obj as Email);

        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);

        public static implicit operator string(Email email) => email.Value;
    }
}
