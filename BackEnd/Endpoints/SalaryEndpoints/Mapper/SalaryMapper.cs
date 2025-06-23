using BackEnd.Endpoints.SalaryEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.SalaryEndpoints.Mapper;

[Mapper]
public static partial class SalaryMapper
{
    public static partial GetSalaryDTO ToGetSalaryDTO(this Salary salary, string paymentDate);
}