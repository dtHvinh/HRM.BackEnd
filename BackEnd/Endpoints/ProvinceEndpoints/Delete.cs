using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.ProvinceEndpoints;

public record DeleteProvinceRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteProvinceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<ProvinceGroup>();
    }

    public override async Task HandleAsync(DeleteProvinceRequest req, CancellationToken ct)
    {
        var province = await _context.Province.FindAsync([req.Id], cancellationToken: ct);

        if (province is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            _context.Province.Remove(province);
            await _context.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}