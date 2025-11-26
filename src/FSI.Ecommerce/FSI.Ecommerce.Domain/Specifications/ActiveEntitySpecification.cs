using FSI.Ecommerce.Domain.Entities;
using System.Linq.Expressions;

namespace FSI.Ecommerce.Domain.Specifications
{
    public sealed class ActiveEntitySpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria => x => x.IsActive;
    }
}