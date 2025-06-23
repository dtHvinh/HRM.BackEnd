using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.AllowanceEndpoints;

public class UpdateAllowanceRequest
{
    public int AllowanceId { get; set; }
    public string AllowanceName { get; set; }
    public decimal AllowanceCoefficient { get; set; }
}

public class Update(ApplicationDbContext context) : Endpoint<UpdateAllowanceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{AllowanceId}");
        Group<AllowanceGroup>();
    }

    public override async Task HandleAsync(UpdateAllowanceRequest req, CancellationToken ct)
    {
        var allowance = await _context.Allowances
            .FirstOrDefaultAsync(a => a.AllowanceId == req.AllowanceId, ct);

        if (allowance == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update the allowance properties
        allowance.AllowanceName = req.AllowanceName;
        allowance.AllowanceCoefficient = req.AllowanceCoefficient;

        await _context.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}