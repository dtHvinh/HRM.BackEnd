using BackEnd.Endpoints.AllowanceEndpoints.DTOs;
using BackEnd.Models;

namespace BackEnd.Endpoints.AllowanceEndpoints.Mapper;

public static class AllowanceMapper
{
    public static GetAllowanceDTO ToGetAllowanceDTO(this Allowance allowance)
    {
        return new GetAllowanceDTO
        {
            AllowanceId = allowance.AllowanceId,
            AllowanceName = allowance.AllowanceName,
            AllowanceCoefficient = allowance.AllowanceCoefficient
        };
    }
}