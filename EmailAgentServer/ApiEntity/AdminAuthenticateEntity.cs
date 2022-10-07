namespace EmailAgentServer.Entity;

public class AdminAuthenticateEntity
{
    public bool IsSuccess { get; set; }
    
    public string? AccessToken { get; set; }
}