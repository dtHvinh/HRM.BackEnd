using BackEnd.Endpoints.EmployeeBenefitEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints.Mapper;

[Mapper]
public static partial class EmployeeBenefitMapper
{
    public static partial GetEmployeeBenefitDTO ToGetEmployeeBenefitDTO(this EmployeeBenefit employeeBenefit, string allowanceName, decimal allowanceCoefficient, string insuranceName, decimal insuranceCoefficient);
}