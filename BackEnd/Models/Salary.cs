using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Salaries")]
public class Salary
{
    [Key] public int SalaryId { get; set; }
    public decimal SalaryCoefficient { get; set; }
}
