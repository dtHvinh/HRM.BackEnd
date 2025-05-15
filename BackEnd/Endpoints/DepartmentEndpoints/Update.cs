using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.DepartmentEndpoints;

public record UpdateDepartmentRequest(int Id, string Name);

public class Update(ApplicationDbContext context) : Endpoint<UpdateDepartmentRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<DepartmentGroup>();
    }

    public override async Task HandleAsync(UpdateDepartmentRequest req, CancellationToken ct)
    {
        var department = await _context.Departments.FindAsync([req.Id], ct);

        if (department is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        department.Name = req.Name;
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}