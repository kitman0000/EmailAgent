
using System.ComponentModel.DataAnnotations;

namespace EmailAgentServer.DBEntity;

public class EmailPlaceholder
{
    [Key]
    public int Id { get; set; }
    
    public EmailTemplate EmailTemplates { get; set; }
    
    public int EmailTemplateId { get; set; }

    public string PlaceHolder { get; set; }
}