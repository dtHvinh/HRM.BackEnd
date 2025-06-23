using BackEnd.Data;
using BackEnd.Endpoints.DepartmentEndpoints.DTOs;
using BackEnd.Endpoints.DepartmentEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.DepartmentEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetDepartmentDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Group<DepartmentGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var departments = await _context.Departments.OrderBy(e => e.Name).Select(e => e.ToGetDepartmentDTO()).ToListAsync(ct);
        await SendAsync(departments, cancellation: ct);
    }
}