using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.WardEndpoints;

public record AddWardRequest(string Name);

public class Add(ApplicationDbContext context) : Endpoint<AddWardRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<WardGroup>();
    }

    public override async Task HandleAsync(AddWardRequest req, CancellationToken ct)
    {
        var ward = new Ward
        {
            WardName = req.Name
        };

        _context.Wards.Add(ward);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}