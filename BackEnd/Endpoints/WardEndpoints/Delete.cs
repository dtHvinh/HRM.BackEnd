using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.WardEndpoints;

public record DeleteWardRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteWardRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<WardGroup>();
    }

    public override async Task HandleAsync(DeleteWardRequest req, CancellationToken ct)
    {
        var ward = await _context.Wards.FindAsync([req.Id], cancellationToken: ct);

        if (ward is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            _context.Wards.Remove(ward);
            await _context.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}