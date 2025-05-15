using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.PositionEndpoints;
public record DeletePositionRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeletePositionRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<PositionGroup>();
    }

    public override async Task HandleAsync(DeletePositionRequest req, CancellationToken ct)
    {
        var position = await _context.Positions.FindAsync([req.Id], cancellationToken: ct);

        if (position is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }
    }
}