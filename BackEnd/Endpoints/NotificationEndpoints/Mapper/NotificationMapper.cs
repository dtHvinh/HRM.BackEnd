using BackEnd.Endpoints.NotificationEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.NotificationEndpoints.Mapper;

[Mapper]
public static partial class NotificationMapper
{
    public static partial GetNotificationDTO ToGetNotificationDTO(this Notification notification, DateTimeOffset notificationDate, string employeeName);
}