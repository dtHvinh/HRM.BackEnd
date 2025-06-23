using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.WardEndpoints.DTOs;
using BackEnd.Endpoints.WardEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.WardEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetWardDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<WardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var wards = await _context.Wards.OrderBy(e => e.WardName).Select(e => e.ToGetWardDTO()).ToListAsync(ct);
        await SendAsync(wards, cancellation: ct);
    }
}