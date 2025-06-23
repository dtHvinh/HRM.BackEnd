using FastEndpoints;

namespace BackEnd.Endpoints.Groups;

public class InsuranceGroup : Group
{
    public InsuranceGroup()
    {
        Configure("insurances", ep =>
        {

        });
    }
}