using BackEnd.Endpoints.ProvinceEndpoints.DTOs;
using BackEnd.Models;
using Riok.Mapperly.Abstractions;

namespace BackEnd.Endpoints.ProvinceEndpoints.Mapper;

[Mapper]
public static partial class ProvinceMapper
{
    public static partial GetProvinceDTO ToGetProvinceDTO(this Province province);
}
