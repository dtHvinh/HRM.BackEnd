namespace BackEnd.Endpoints.NotificationEndpoints.DTOs;

public class GetNotificationDTO
{
    public int NotificationId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTimeOffset NotificationDate { get; set; }
}