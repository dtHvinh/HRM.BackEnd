using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Wards")]
public class Ward
{
    [Key] public int WardId { get; set; }
    public required string WardName { get; set; }
}
