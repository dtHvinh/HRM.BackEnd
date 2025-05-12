using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Employees")]
public class Employee
{
    [Key] public int EmployeeId { get; set; }
    public required string FullName { get; set; }
    public DateTimeOffset DOB { get; set; }
    public Gender Gender { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}

public enum Gender
{
    Male,
    Female,
    Other
}