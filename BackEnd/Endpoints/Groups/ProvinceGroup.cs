using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class ProvinceGroup : Group
{
    public ProvinceGroup()
    {
        Configure("provinces", cf =>
        {
            cf.AllowAnonymous();
        });
    }
}
