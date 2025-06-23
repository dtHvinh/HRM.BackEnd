using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.InsuranceEndpoints;

public record AddInsuranceRequest(string InsuranceName, decimal InsuranceCoefficient);

public class Add(ApplicationDbContext context) : Endpoint<AddInsuranceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<InsuranceGroup>();
    }

    public override Task HandleAsync(AddInsuranceRequest req, CancellationToken ct)
    {
        var insurance = new Insurance { InsuranceName = req.InsuranceName, InsuranceCoefficient = req.InsuranceCoefficient };
        _context.Insurances.Add(insurance);
        return _context.SaveChangesAsync(ct);
    }
}
