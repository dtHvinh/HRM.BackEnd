using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.ProvinceEndpoints;

public record AddProvinceRequest(string Name);

public class Add(ApplicationDbContext context) : Endpoint<AddProvinceRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<ProvinceGroup>();
    }

    public override async Task HandleAsync(AddProvinceRequest req, CancellationToken ct)
    {
        var province = new Province
        {
            ProvinceName = req.Name
        };

        _context.Province.Add(province);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(new { Id = province.ProvinceId }, ct);
    }
}
