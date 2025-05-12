using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.WardEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<Ward>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<WardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var wards = await _context.Wards.ToListAsync(ct);
        await SendAsync(wards, cancellation: ct);
    }
}