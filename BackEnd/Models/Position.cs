using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Positions")]
public class Position
{
    [Key] public int PositionId { get; set; }
    public required string Name { get; set; }
}
