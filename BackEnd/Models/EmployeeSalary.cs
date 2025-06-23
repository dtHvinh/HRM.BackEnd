using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("EmployeeSalaries")]
[PrimaryKey(nameof(EmployeeId), nameof(SalaryId))]
public class EmployeeSalary
{
    public int EmployeeId { get; set; }
    public int SalaryId { get; set; }
    public DateOnly PaymentDate { get; set; }

    public Employee? Employee { get; set; }
    public Salary? Salary { get; set; }
}
