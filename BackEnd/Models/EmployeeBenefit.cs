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
}
