namespace BlogApp.Domain.Entities;

public class TokenJwt
{
    public string IdUser { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
