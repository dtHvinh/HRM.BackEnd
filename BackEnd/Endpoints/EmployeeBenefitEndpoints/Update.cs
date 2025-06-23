using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeBenefitEndpoints;

public class UpdateEmployeeBenefitRequest
{
    public int EmployeeId { get; set; }
    public int AllowanceId { get; set; }
    public int InsuranceId { get; set; }
    public DateTimeOffset JoinedDate { get; set; }
}

public class Update(ApplicationDbContext context) : Endpoint<UpdateEmployeeBenefitRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{EmployeeId}/{AllowanceId}/{InsuranceId}");
        Group<EmployeeBenefitGroup>();
    }

    public override async Task HandleAsync(UpdateEmployeeBenefitRequest req, CancellationToken ct)
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

        // Update the joined date
        employeeBenefit.JoinedDate = req.JoinedDate;

        await _context.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}