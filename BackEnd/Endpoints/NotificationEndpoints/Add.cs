using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.NotificationEndpoints;

public record AddNotificationRequest(string Content, int EmployeeId);

public class Add(ApplicationDbContext context) : Endpoint<AddNotificationRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<NotificationGroup>();
    }

    public override async Task HandleAsync(AddNotificationRequest req, CancellationToken ct)
    {
        var notification = new Notification
        {
            Content = req.Content,
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(ct);

        var employeeNotification = new EmployeeNotification
        {
            EmployeeId = req.EmployeeId,
            NotificationId = notification.NotificationId,
            NotificationDate = DateTimeOffset.UtcNow
        };

        _context.EmployeeNotifications.Add(employeeNotification);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}