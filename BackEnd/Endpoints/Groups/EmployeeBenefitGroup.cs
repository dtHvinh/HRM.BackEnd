using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class EmployeeBenefitGroup : Group
{
    public EmployeeBenefitGroup()
    {
        Configure("benefits", ep =>
        {
        });
    }
}