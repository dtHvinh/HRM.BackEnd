using BackEnd.Endpoints.EmployeeEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.EmployeeEndpoints.Mapper;

[Mapper]
public static partial class EmployeeMapper
{
    public static partial GetEmployeeDTO ToGetEmployeeDTO(this Employee employee, string department, string position, string province, string ward);
}