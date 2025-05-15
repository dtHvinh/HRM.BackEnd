using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.ProvinceEndpoints.DTOs;
using BackEnd.Endpoints.ProvinceEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.ProvinceEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetProvinceDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<ProvinceGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var provinces = await _context.Province.OrderBy(e => e.ProvinceName).Select(e => e.ToGetProvinceDTO()).ToListAsync(ct);
        await SendAsync(provinces, cancellation: ct);
    }
}