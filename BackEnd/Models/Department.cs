using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Departments")]
public class Department
{
    [Key] public int DepartmentId { get; set; }
    public required string Name { get; set; }
}
