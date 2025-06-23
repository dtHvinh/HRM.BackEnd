using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public class DeleteEmployeeBenefitRequest
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public int InsuranceId { get; set; }
}

public class Delete(ApplicationDbContext context) : Endpoint<DeleteEmployeeBenefitRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{EmployeeId}/{AllowanceId}/{InsuranceId}");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(DeleteEmployeeBenefitRequest req, CancellationToken ct)
    {
        var employeeBenefit = await _context.EmployeeBenefits
            .FirstOrDefaultAsync(eb =>
                eb.EmployeeId == req.EmployeeId &&
                eb.AllowanceId == req.AllowanceId &&
                eb.InsuranceId == req.InsuranceId,
                ct);

        if (employeeBenefit == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.EmployeeBenefits.Remove(employeeBenefit);
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}