namespace EmailAgentServer.Entity;

public class ApplicationCreateTokenEntity
{
    public string token { get; set; }
    
    public DateTime ExpireDateTime { get; set; }
}