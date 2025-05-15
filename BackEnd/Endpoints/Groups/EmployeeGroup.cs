using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class EmployeeGroup : Group
{
    public EmployeeGroup()
    {
        Configure("employees", cf =>
        {
        });
    }
}