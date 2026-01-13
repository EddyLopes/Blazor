namespace Application;

public class JwtSettings
{
    public string secret { get; set; }
    public int TokenExpiryTimeInMinutes { get; set; }
    public int RefreshTokenExpiryTimeInDays { get; set; }
}
