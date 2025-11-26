using System;

namespace FSI.Ecommerce.Domain.ValueObjects
{
    /// <summary>
    /// Value Object para representar valores monetários com moeda.
    /// Imutável e com operações básicas de soma e multiplicação.
    /// </summary>
    public sealed record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));

            Amount = amount;
            Currency = currency.ToUpperInvariant();
        }

        /// <summary>
        /// Fábrica estática: cria um Money a partir de valor e moeda.
        /// </summary>
        public static Money From(decimal amount, string currency)
            => new(amount, currency);

        /// <summary>
        /// Retorna zero na moeda informada.
        /// </summary>
        public static Money Zero(string currency) => new(0m, currency);

        /// <summary>
        /// Soma dois Money com a mesma moeda.
        /// </summary>
        public Money Add(Money other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Cannot add Money with different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        /// <summary>
        /// Multiplica o valor por um fator inteiro (ex: quantidade).
        /// </summary>
        public Money Multiply(int factor)
        {
            if (factor < 0)
                throw new ArgumentOutOfRangeException(nameof(factor), "Factor must be non-negative.");

            return new Money(Amount * factor, Currency);
        }

        /// <summary>
        /// Multiplica o valor por um fator decimal (se precisar).
        /// </summary>
        public Money Multiply(decimal factor)
        {
            if (factor < 0)
                throw new ArgumentOutOfRangeException(nameof(factor), "Factor must be non-negative.");

            return new Money(Amount * factor, Currency);
        }

        public override string ToString() => $"{Amount:0.00} {Currency}";
    }
}
