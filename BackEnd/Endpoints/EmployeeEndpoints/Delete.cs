using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record DeleteEmployeeRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteEmployeeRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(DeleteEmployeeRequest req, CancellationToken ct)
    {
        var employee = await _context.Employees.FindAsync([req.Id], cancellationToken: ct);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Remove related records first
        var employeeDepartments = await _context.EmployeeDepartments
            .Where(ed => ed.EmployeeId == req.Id)
            .ToListAsync(ct);
        _context.EmployeeDepartments.RemoveRange(employeeDepartments);

        var employeeAddresses = await _context.EmployeeAddresses
            .Where(ea => ea.EmployeeId == req.Id)
            .ToListAsync(ct);
        _context.EmployeeAddresses.RemoveRange(employeeAddresses);

        // Remove the employee
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}