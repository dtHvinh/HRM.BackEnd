using BackEnd.Endpoints.PositionEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.PositionEndpoints.Mapper;

[Mapper]
public static partial class PositionMapper
{
    public static partial GetPositionDTO ToGetPositionDTO(this Position position);
}