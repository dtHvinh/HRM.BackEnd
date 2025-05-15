using BackEnd.Endpoints.DepartmentEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.DepartmentEndpoints.Mapper;

[Mapper]
public static partial class DeparmentMapper
{
    public static partial GetDepartmentDTO ToGetDepartmentDTO(this Department department);
}
