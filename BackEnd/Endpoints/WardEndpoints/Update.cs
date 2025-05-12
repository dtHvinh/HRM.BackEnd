using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.WardEndpoints;

public record UpdateWardRequest(int Id, string Name);

public class Update(ApplicationDbContext context) : Endpoint<UpdateWardRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<WardGroup>();
    }

    public override async Task HandleAsync(UpdateWardRequest req, CancellationToken ct)
    {
        var ward = await _context.Wards.FindAsync([req.Id], ct);

        if (ward is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        ward.WardName = req.Name;
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}