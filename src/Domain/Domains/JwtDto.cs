namespace Domain.Domains;

public class JwtDto
{
    public JwtDto(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }

    public string Token { get; }
    public string RefreshToken { get; }
}