using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.DepartmentEndpoints;

public record AddDepartmentRequest(string Name);

public class Add(ApplicationDbContext context) : Endpoint<AddDepartmentRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<DepartmentGroup>();
    }

    public override async Task HandleAsync(AddDepartmentRequest req, CancellationToken ct)
    {
        var department = new Department
        {
            Name = req.Name
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(new { Id = department.DepartmentId }, ct);
    }
}