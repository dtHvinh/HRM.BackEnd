using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Allowances")]
public class Allowance
{
    [Key] public int AllowanceId { get; set; }
    public required string AllowanceName { get; set; }
    public decimal AllowanceCoefficient { get; set; }
}
