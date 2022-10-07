using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailAgentServer.DBEntity;

[Microsoft.EntityFrameworkCore.Index(nameof(UserName),IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
    
    public bool IsRoot { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string lastUpdateTime { get; set; }
}