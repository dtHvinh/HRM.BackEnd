using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.PositionEndpoints;

public record AddPositionRequest(string Name);

public class Add(ApplicationDbContext context) : Endpoint<AddPositionRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<PositionGroup>();
    }

    public override async Task HandleAsync(AddPositionRequest req, CancellationToken ct)
    {
        var position = new Position
        {
            Name = req.Name
        };

        _context.Positions.Add(position);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(new { Id = position.PositionId }, ct);
    }
}
