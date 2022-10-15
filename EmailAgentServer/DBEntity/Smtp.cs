using System.ComponentModel.DataAnnotations;

namespace EmailAgentServer.DBEntity;

public class Smtp
{
    [Key]
    public int Id { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool EnableSsl { get; set; }

    public DateTime LastUpdateTime { get; set; }

    public Application Application {get;set;}

    public int ApplicationId {get;set;}
}