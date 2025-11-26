using FSI.Ecommerce.Domain.Entities;
using FSI.Ecommerce.Domain.Interfaces;
using FSI.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FSI.Ecommerce.Infrastructure.Repositories
{
    public sealed class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(EcommerceDbContext context) : base(context)
        {
        }

        public Task<Account?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return DbSet.FirstOrDefaultAsync(a => a.Email == email, ct);
        }
    }
}