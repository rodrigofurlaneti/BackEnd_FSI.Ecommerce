using FSI.Ecommerce.Domain.Entities;
using System.Linq.Expressions;

namespace FSI.Ecommerce.Domain.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}