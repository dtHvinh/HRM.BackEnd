using BackEnd.Data;
using BackEnd.Endpoints.AllowanceEndpoints.DTOs;
using BackEnd.Endpoints.AllowanceEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.AllowanceEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetAllowanceDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<AllowanceGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var allowances = await _context.Allowances.OrderBy(a => a.AllowanceId).Select(a => a.ToGetAllowanceDTO()).ToListAsync(ct);
        await SendAsync(allowances, cancellation: ct);
    }
}