using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("EmployeeNotifications")]
[PrimaryKey(nameof(EmployeeId), nameof(NotificationId))]
public class EmployeeNotification
{
    public int EmployeeId { get; set; }
    public int NotificationId { get; set; }
    public DateTimeOffset NotificationDate { get; set; }

    public Notification? Notification { get; set; }
    public Employee? Employee { get; set; }
}
