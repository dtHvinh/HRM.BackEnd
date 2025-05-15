using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class WardGroup : Group
{
    public WardGroup()
    {
        Configure("wards", cf =>
        {
        });
    }
}