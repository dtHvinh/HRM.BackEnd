using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class SalaryGroup : Group
{
    public SalaryGroup()
    {
        Configure("salaries", ep =>
        {
        });
    }
}