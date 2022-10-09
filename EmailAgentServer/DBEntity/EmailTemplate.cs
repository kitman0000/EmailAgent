using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailAgentServer.DBEntity;

[Microsoft.EntityFrameworkCore.Index(nameof(TemplateName),IsUnique = true)]
public class EmailTemplate
{
    [Key]
    public int Id { get; set; }
    
    public string TemplateName { get; set; }
    
    public string Subject { get; set; }
    
    public string FilePath { get; set; }

    public List<EmailPlaceholder> EmailPlaceholders { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string LastUpdateTime { get; set; }
}