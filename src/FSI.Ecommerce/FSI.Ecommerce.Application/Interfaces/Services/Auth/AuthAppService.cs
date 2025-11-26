using System.Security.Cryptography;
using System.Text;
using FSI.Ecommerce.Application.Dtos.Auth;
using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Interfaces.Services;
using FSI.Ecommerce.Domain.Interfaces;

namespace FSI.Ecommerce.Application.Interfaces.Services.Auth
{
    public sealed class AuthAppService : IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthAppService(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<ResultDto<LoginResponseDto>> LoginAsync(
            LoginRequestDto request,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return ResultDto<LoginResponseDto>.Fail("Email and password are required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email, ct);

            if (user is null || !user.IsActive)
                return ResultDto<LoginResponseDto>.Fail("Invalid credentials.");

            var incomingHash = ComputeSha256(request.Password);

            if (!string.Equals(user.PasswordHash, incomingHash, StringComparison.Ordinal))
                return ResultDto<LoginResponseDto>.Fail("Invalid credentials.");

            var token = _tokenService.GenerateAccessToken(user, out var expiresAt);

            var response = new LoginResponseDto
            {
                AccessToken = token,
                ExpiresAt = expiresAt
            };

            return ResultDto<LoginResponseDto>.Ok(response);
        }

        private static string ComputeSha256(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}