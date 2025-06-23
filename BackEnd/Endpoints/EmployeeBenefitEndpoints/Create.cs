using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public record CreateEmployeeBenefitRequest(int EmployeeId, int AllowanceId, int InsuranceId);

public class Create(ApplicationDbContext context) : Endpoint<CreateEmployeeBenefitRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("/{employeeId}");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(CreateEmployeeBenefitRequest req, CancellationToken ct)
    {
        // Check if the employee benefit already exists
        var existingBenefit = await _context.EmployeeBenefits
            .FirstOrDefaultAsync(eb =>
                eb.EmployeeId == req.EmployeeId &&
                eb.AllowanceId == req.AllowanceId &&
                eb.InsuranceId == req.InsuranceId,
                ct);

        if (existingBenefit != null)
        {
            AddError("Benefit already exists for this employee, allowance, and insurance combination.");
            await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
            return;
        }

        // Create new employee benefit
        var newEmployeeBenefit = new EmployeeBenefit
        {
            EmployeeId = req.EmployeeId,
            AllowanceId = req.AllowanceId,
            InsuranceId = req.InsuranceId,
            JoinedDate = DateTimeOffset.UtcNow,
        };

        _context.EmployeeBenefits.Add(newEmployeeBenefit);
        await _context.SaveChangesAsync(ct);

        await SendCreatedAtAsync<GetById>(new { req.EmployeeId, req.AllowanceId, req.InsuranceId }, null, cancellation: ct);
    }
}