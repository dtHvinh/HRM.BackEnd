using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.ProvinceEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<Province>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<ProvinceGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var provinces = await _context.Province.ToListAsync(ct);
        await SendAsync(provinces, cancellation: ct);
    }
}