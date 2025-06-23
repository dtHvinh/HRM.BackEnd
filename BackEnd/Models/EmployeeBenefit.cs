using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("EmployeeBenefits")]
[PrimaryKey(nameof(EmployeeId), nameof(AllowanceId), nameof(InsuranceId))]
public class EmployeeBenefit
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public int InsuranceId { get; set; }
    public DateTimeOffset JoinedDate { get; set; }

    public Employee Employee { get; set; } = default!;
    public Allowance Allowance { get; set; } = default!;
    public Insurance Insurance { get; set; } = default!;

}
