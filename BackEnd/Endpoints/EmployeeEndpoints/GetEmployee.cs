using BackEnd.Data;
using BackEnd.Endpoints.EmployeeEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record DepartmetHistory(int DepartmentId, string Name, DateTimeOffset TransferInDate, DateTimeOffset? TransferOutDate, DateTimeOffset AppointmentDate, string Position);

public record GetEmployeeRequest(int Id);

public record GetEmployeeDetailResponse(int EmployeeId, string FullName, DateTimeOffset DOB, string Gender, string Email, string Phone, string Position, string Province, string Ward, List<DepartmetHistory> Departments);

public class GetEmployee(ApplicationDbContext context) : Endpoint<GetEmployeeRequest, GetEmployeeDetailResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("/{id}");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(GetEmployeeRequest req, CancellationToken ct)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeDepartments)
            .Include(e => e.EmployeeAddress).FirstOrDefaultAsync(e => e.EmployeeId == req.Id, ct);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(employee.ToGetDetailResponse(
            employee.EmployeeDepartments!.OrderByDescending(e => e.AppointmentDate).First()!.Position!.Name,
            employee.EmployeeAddress!.Province!.ProvinceName,
            employee.EmployeeAddress!.Ward!.WardName,
            employee.EmployeeDepartments!.OrderByDescending(e => e.TransferInDate).Select(
                ed => new DepartmetHistory(
                    ed.Department!.DepartmentId,
                    ed.Department.Name,
                    ed.TransferInDate,
                    ed.TransferOutDate == DateTimeOffset.MinValue ? null : ed.TransferOutDate,
                    ed.AppointmentDate,
                    ed.Position!.Name)
                ).ToList()), ct);
    }
}
