using BackEnd.Endpoints.InsuranceEndpoints.DTOs;
using BackEnd.Models;

namespace BackEnd.Endpoints.InsuranceEndpoints.Mapper;

public static class InsuranceMapper
{
    public static GetInsuranceDTO ToGetInsuranceDTO(this Insurance insurance)
    {
        return new GetInsuranceDTO
        {
            InsuranceId = insurance.InsuranceId,
            InsuranceName = insurance.InsuranceName,
            InsuranceCoefficient = insurance.InsuranceCoefficient
        };
    }
}