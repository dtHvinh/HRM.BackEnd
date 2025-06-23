using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.InsuranceEndpoints.DTOs;
using BackEnd.Endpoints.InsuranceEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.InsuranceEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetInsuranceDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<InsuranceGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var insurances = await _context.Insurances.OrderBy(i => i.InsuranceId).Select(i => i.ToGetInsuranceDTO()).ToListAsync(ct);
        await SendAsync(insurances, cancellation: ct);
    }
}