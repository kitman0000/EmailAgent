using System.Text.Json;

namespace EmailAgentServer.Entity;

public class ApplicationTokenEntity
{
    public int applicationId { get; set;}

    public string applicationName { get; set; }

    public DateTime expireDate;

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static ApplicationTokenEntity? FromJson(string json)
    {
        return JsonSerializer.Deserialize<ApplicationTokenEntity>(json);
    }
}