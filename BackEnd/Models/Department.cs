using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Departments")]
[Index(nameof(Name), IsUnique = true)]
public class Department
{
    [Key] public int DepartmentId { get; set; }
    public required string Name { get; set; }
}
