namespace FSI.Ecommerce.Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency must be provided.", nameof(currency));

            Amount = amount;
            Currency = currency.ToUpperInvariant();
        }

        public static Money Zero(string currency) => new(0m, currency);

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int factor) => new(Amount * factor, Currency);

        private void EnsureSameCurrency(Money other)
        {
            if (!Currency.Equals(other.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Cannot operate with different currencies.");
        }

        public bool Equals(Money? other)
        {
            if (other is null) return false;
            return Amount == other.Amount &&
                   Currency.Equals(other.Currency, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as Money);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public override string ToString() => $"{Amount:0.00} {Currency}";
    }
}
