using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.PositionEndpoints.DTOs;
using BackEnd.Endpoints.PositionEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.PositionEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetPositionDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<PositionGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var positions = await _context.Positions.OrderBy(p => p.Name).Select(p => p.ToGetPositionDTO()).ToListAsync(ct);
        await SendAsync(positions, cancellation: ct);
    }
}