namespace FlavorTalk.Shared;

public class AppSettings
{
    public required JwtConfigs Jwt { get; set; }
}

public class JwtConfigs
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}
