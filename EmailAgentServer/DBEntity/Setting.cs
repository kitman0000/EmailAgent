using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailAgentServer.DBEntity;

public class Setting
{
    [Key]
    public int Id { get; set; }
    
    public string key { get; set; }
    
    public string value { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string lastUpdateTime { get; set; }
}
