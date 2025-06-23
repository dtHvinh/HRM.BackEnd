using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.NotificationEndpoints.DTOs;
using BackEnd.Endpoints.NotificationEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.NotificationEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetNotificationDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<NotificationGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var notifications = await _context.EmployeeNotifications.Select(e => e.Notification!.ToGetNotificationDTO(e.NotificationDate, e.Employee!.FullName)).ToListAsync(ct);

        await SendAsync(notifications, cancellation: ct);
    }
}