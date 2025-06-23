using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.InsuranceEndpoints;


public record DeleteInsuranceRequest(int InsuranceId);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteInsuranceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{insuranceId}");
        Group<InsuranceGroup>();
    }

    public override async Task HandleAsync(DeleteInsuranceRequest req, CancellationToken ct)
    {
        var insurance = await _context.Insurances.FirstOrDefaultAsync(a => a.InsuranceId == req.InsuranceId, ct);

        if (insurance == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Insurances.Remove(insurance);


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
