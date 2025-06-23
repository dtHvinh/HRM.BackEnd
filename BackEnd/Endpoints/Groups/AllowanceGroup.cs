using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class AllowanceGroup : Group
{
    public AllowanceGroup()
    {
        Configure("allowances", ep =>
        {

        });
    }
}