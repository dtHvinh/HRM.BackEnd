using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.AllowanceEndpoints;

public record DeleteAllowanceRequest(int AllowanceId);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteAllowanceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{allowanceId}");
        Group<AllowanceGroup>();
    }

    public override async Task HandleAsync(DeleteAllowanceRequest req, CancellationToken ct)
    {
        var allowance = await _context.Allowances.FirstOrDefaultAsync(a => a.AllowanceId == req.AllowanceId, ct);

        if (allowance == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Allowances.Remove(allowance);


        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}
