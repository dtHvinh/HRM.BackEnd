using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BackEnd.Models;

[Table("EmployeeAddresses")]
[PrimaryKey(nameof(EmployeeId), nameof(ProvinceId), nameof(WardId))]
[DebuggerDisplay("EmployeeId: {EmployeeId}")]
public class EmployeeAddress
{
    public int ProvinceId { get; set; }
    public int WardId { get; set; }
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }
    public Province? Province { get; set; }
    public Ward? Ward { get; set; }
}
