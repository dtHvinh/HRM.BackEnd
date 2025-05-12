using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("auth", cf =>
        {
            cf.AllowAnonymous();
        });
    }
}
