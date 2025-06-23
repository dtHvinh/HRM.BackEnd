using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.AllowanceEndpoints;

public record AddAllowanceRequest(string AllowanceName, decimal AllowanceCoefficient);

public class Add(ApplicationDbContext context) : Endpoint<AddAllowanceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<AllowanceGroup>();
    }

    public override Task HandleAsync(AddAllowanceRequest req, CancellationToken ct)
    {
        var allowance = new Allowance { AllowanceName = req.AllowanceName, AllowanceCoefficient = req.AllowanceCoefficient };
        _context.Allowances.Add(allowance);
        return _context.SaveChangesAsync(ct);
    }
}