using BackEnd.Data;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.DTOs;
using BackEnd.Endpoints.EmployeeBenefitEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public class GetByIdRequest
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public int InsuranceId { get; set; }
}

public class GetById(ApplicationDbContext context) : Endpoint<GetByIdRequest, GetEmployeeBenefitDTO?>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{EmployeeId}/{AllowanceId}/{InsuranceId}");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(GetByIdRequest req, CancellationToken ct)
    {
        var eb = await _context.EmployeeBenefits
            .Include(eb => eb.Employee)
            .Include(eb => eb.Allowance)
            .Include(eb => eb.Insurance)
            .FirstOrDefaultAsync(eb =>
                eb.EmployeeId == req.EmployeeId &&
                eb.AllowanceId == req.AllowanceId &&
                eb.InsuranceId == req.InsuranceId,
                ct);

        if (eb == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(eb.ToGetEmployeeBenefitDTO(eb.Allowance.AllowanceName, eb.Allowance.AllowanceCoefficient, eb.Insurance.InsuranceName, eb.Insurance.InsuranceCoefficient), cancellation: ct);
    }
}