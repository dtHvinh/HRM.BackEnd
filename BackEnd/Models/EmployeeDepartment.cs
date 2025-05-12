using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("EmployeeDepartments")]
[PrimaryKey(nameof(EmployeeId), nameof(DepartmentId), nameof(PositionId))]
public class EmployeeDepartment
{
    public int EmployeeId { get; set; }
    public int DepartmentId { get; set; }
    public int PositionId { get; set; }
    public DateTimeOffset TransferInDate { get; set; }
    public DateTimeOffset TransferOutDate { get; set; }
    public DateTimeOffset AppointmentDate { get; set; }
}
