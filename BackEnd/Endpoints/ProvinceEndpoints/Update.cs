using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.ProvinceEndpoints;

public record UpdateProvinceRequest(int Id, string Name);

public class Update(ApplicationDbContext context) : Endpoint<UpdateProvinceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<ProvinceGroup>();
    }

    public override async Task HandleAsync(UpdateProvinceRequest req, CancellationToken ct)
    {
        var province = await _context.Province.FindAsync([req.Id], ct);

        if (province is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        province.ProvinceName = req.Name;
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}