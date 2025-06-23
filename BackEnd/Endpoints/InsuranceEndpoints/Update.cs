using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.InsuranceEndpoints;

public record UpdateInsuranceRequest(int InsuranceId, string InsuranceName, decimal InsuranceCoefficient);

public class Update(ApplicationDbContext context) : Endpoint<UpdateInsuranceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{insuranceId}");
        Group<InsuranceGroup>();
    }

    public override async Task HandleAsync(UpdateInsuranceRequest req, CancellationToken ct)
    {
        var insurance = await _context.Insurances
            .FirstOrDefaultAsync(i => i.InsuranceId == req.InsuranceId, ct);

        if (insurance == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update the insurance properties
        insurance.InsuranceName = req.InsuranceName;
        insurance.InsuranceCoefficient = req.InsuranceCoefficient;

        await _context.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}