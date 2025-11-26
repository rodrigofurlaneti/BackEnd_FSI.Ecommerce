namespace FSI.Ecommerce.Application.Dtos.Auth
{
    public sealed class LoginResponseDto
    {
        public string AccessToken { get; init; } = null!;
        public DateTime ExpiresAt { get; init; }
    }
}
