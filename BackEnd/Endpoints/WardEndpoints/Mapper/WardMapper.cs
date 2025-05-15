using BackEnd.Endpoints.WardEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.WardEndpoints.Mapper;

[Mapper]
public static partial class WardMapper
{
    public static partial GetWardDTO ToGetWardDTO(this Ward ward);
}
