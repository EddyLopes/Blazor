namespace App.Infrastructure;

public class ApiSettings
{
    public string BaseApiUrl { get; set; }
    public TokenEndpoints TokenEndpoints { get; set; }
}

public class TokenEndpoints
{
    public string Login { get; set; }
    public string RefreshToken { get; set; }
}
