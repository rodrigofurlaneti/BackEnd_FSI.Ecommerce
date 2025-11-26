using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EcommerceDbContext context) : base(context)
        {
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return DbSet.FirstOrDefaultAsync(u => u.Email == email, ct);
        }
    }
}
