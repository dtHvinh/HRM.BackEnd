using BackEnd.Data;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.DTOs;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public class GetByEmployeeIdRequest
{
    public int EmployeeId { get; set; }
}

public class GetByEmployeeId(ApplicationDbContext context) : Endpoint<GetByEmployeeIdRequest, List<GetEmployeeBenefitDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("employee/{EmployeeId}");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(GetByEmployeeIdRequest req, CancellationToken ct)
    {
        var employeeBenefits = await _context.EmployeeBenefits
            .Include(eb => eb.Employee)
            .Include(eb => eb.Allowance)
            .Include(eb => eb.Insurance)
            .OrderByDescending(eb => eb.JoinedDate)
            .Where(eb => eb.EmployeeId == req.EmployeeId)
            .Select(eb => eb.ToGetEmployeeBenefitDTO(eb.Allowance.AllowanceName, eb.Allowance.AllowanceCoefficient, eb.Insurance.InsuranceName, eb.Insurance.InsuranceCoefficient))
            .ToListAsync(ct);

        if (employeeBenefits is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(employeeBenefits, cancellation: ct);
    }
}