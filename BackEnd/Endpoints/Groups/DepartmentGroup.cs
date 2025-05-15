using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class DepartmentGroup : Group
{
    public DepartmentGroup()
    {
        Configure("departments", cf =>
        {
        });
    }
}