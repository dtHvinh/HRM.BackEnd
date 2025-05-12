using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Notifications")]
public class Notification
{
    [Key] public int NotificationId { get; set; }
    public required string Content { get; set; }
}
