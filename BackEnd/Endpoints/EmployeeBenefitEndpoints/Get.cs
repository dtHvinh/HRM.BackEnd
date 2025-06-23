using BackEnd.Data;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.DTOs;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetEmployeeBenefitDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var employeeBenefits = await _context.EmployeeBenefits
            .Include(eb => eb.Employee)
            .Include(eb => eb.Allowance)
            .Include(eb => eb.Insurance)
            .OrderBy(eb => eb.Employee.FullName)
            .Select(eb => eb.ToGetEmployeeBenefitDTO(eb.Allowance.AllowanceName, eb.Allowance.AllowanceCoefficient, eb.Insurance.InsuranceName, eb.Insurance.InsuranceCoefficient))
            .ToListAsync(ct);

        await SendAsync(employeeBenefits, cancellation: ct);
    }
}