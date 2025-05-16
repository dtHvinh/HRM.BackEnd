using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record TransferRequest(int EmployeeId, int PositionId, int DepartmentId);

public class Transfer(ApplicationDbContext context) : Endpoint<TransferRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{employeeId}/transfer");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(TransferRequest req, CancellationToken ct)
    {
        if (req.EmployeeId == default)
            await SendNotFoundAsync(ct);

        var employee = await _context.Employees.Include(e => e.EmployeeDepartments)
            .FirstOrDefaultAsync(e => e.EmployeeId == req.EmployeeId, ct);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var currentDepartment = employee.EmployeeDepartments!.OrderByDescending(e => e.AppointmentDate)!.First()!;

        if ((req.DepartmentId != default && req.DepartmentId != currentDepartment.DepartmentId)
         || (req.PositionId != default && req.PositionId != currentDepartment.PositionId))
        {
            currentDepartment.TransferOutDate = DateTimeOffset.UtcNow;

            _context.Update(currentDepartment);

            var employeeDepartment = new EmployeeDepartment
            {
                EmployeeId = employee.EmployeeId,
                PositionId = req.PositionId != default ? req.PositionId : currentDepartment.PositionId,
                DepartmentId = req.DepartmentId != default ? req.DepartmentId : currentDepartment.DepartmentId,
                TransferInDate = DateTimeOffset.UtcNow,
                AppointmentDate = DateTimeOffset.UtcNow
            };

            _context.Add(employeeDepartment);

            await _context.SaveChangesAsync(ct);
        }

        await SendOkAsync(ct);
    }
}