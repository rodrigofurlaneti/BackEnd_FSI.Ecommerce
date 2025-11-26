using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.Ecommerce.Domain.Primitives
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is not ValueObject other)
                return false;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Where(x => x is not null)
                .Aggregate(0, (hash, component) =>
                {
                    unchecked
                    {
                        return (hash * 397) ^ component!.GetHashCode();
                    }
                });
        }

        public static bool operator ==(ValueObject? a, ValueObject? b)
            => a is null && b is null || a is not null && a.Equals(b);

        public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);
    }
}
