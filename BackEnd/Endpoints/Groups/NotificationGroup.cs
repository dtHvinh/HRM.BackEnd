using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class NotificationGroup : Group
{
    public NotificationGroup()
    {
        Configure("notifications", ep =>
        {

        });
    }
}