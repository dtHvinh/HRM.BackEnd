using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.DepartmentEndpoints;

public record DeleteDepartmentRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteDepartmentRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<DepartmentGroup>();
    }

    public override async Task HandleAsync(DeleteDepartmentRequest req, CancellationToken ct)
    {
        var department = await _context.Departments.FindAsync([req.Id], cancellationToken: ct);

        if (department is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}