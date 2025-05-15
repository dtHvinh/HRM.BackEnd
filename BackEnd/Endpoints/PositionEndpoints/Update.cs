using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.PositionEndpoints;

public record UpdatePositionRequest(int Id, string Name);

public class Update(ApplicationDbContext context) : Endpoint<UpdatePositionRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<PositionGroup>();
    }

    public override async Task HandleAsync(UpdatePositionRequest req, CancellationToken ct)
    {
        var position = await _context.Positions.FindAsync([req.Id], ct);

        if (position is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        position.Name = req.Name;
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
