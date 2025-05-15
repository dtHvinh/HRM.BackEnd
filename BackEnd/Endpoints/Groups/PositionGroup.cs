using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class PositionGroup : Group
{
    public PositionGroup()
    {
        Configure("positions", cf =>
        {
        });
    }
}