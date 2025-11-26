using FSI.Ecommerce.Domain.Entities;

namespace FSI.Ecommerce.Application.Interfaces.Services
{
    /// <summary>
    /// Implementação ficará na camada Infrastructure, usando JWT.
    /// </summary>
    public interface ITokenService
    {
        string GenerateAccessToken(User user, out DateTime expiresAt);
    }
}
