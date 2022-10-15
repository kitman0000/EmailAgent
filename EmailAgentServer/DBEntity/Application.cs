using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailAgentServer.DBEntity;

[Microsoft.EntityFrameworkCore.Index(nameof(applicationName),IsUnique = true)]
public class Application
{
    [Key]
    public int Id { get; set; }
    
    public string applicationName { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string lastUpdateTime { get; set; }

    public List<Smtp> SmtpList {get;set;}
}