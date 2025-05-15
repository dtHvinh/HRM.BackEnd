using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class AccountGroup : Group
{
    public AccountGroup()
    {
        Configure("accounts", cf =>
        {
        });
    }
}
