using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record UpdateEmployeeRequest
{
    public int Id { get; init; }
    public required string FullName { get; init; }
    public required string DateOfBirth { get; init; }
    public required string Gender { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Department { get; init; }
    public required string Position { get; init; }
    public required string ProvinceId { get; init; }
    public required string WardId { get; init; }
}

public class Update(ApplicationDbContext context) : Endpoint<UpdateEmployeeRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(UpdateEmployeeRequest req, CancellationToken ct)
    {
        var employee = await _context.Employees.FindAsync([req.Id], ct);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update employee basic information
        employee.FullName = req.FullName;
        employee.DOB = DateTimeOffset.Parse(req.DateOfBirth);
        employee.Gender = Enum.Parse<Gender>(req.Gender);
        employee.Email = req.Email;
        employee.Phone = req.Phone;

        // Update department relationship
        if (int.TryParse(req.Department, out int departmentId))
        {
            var employeeDepartment = await _context.EmployeeDepartments
                .FirstOrDefaultAsync(ed => ed.EmployeeId == req.Id, ct);

            if (employeeDepartment != null)
            {
                employeeDepartment.DepartmentId = departmentId;
            }
            else
            {
                _context.EmployeeDepartments.Add(new EmployeeDepartment
                {
                    EmployeeId = req.Id,
                    DepartmentId = departmentId
                });
            }
        }

        // Update address information
        if (int.TryParse(req.ProvinceId, out int provinceId) && int.TryParse(req.WardId, out int wardId))
        {
            var employeeAddress = await _context.EmployeeAddresses
                .FirstOrDefaultAsync(ea => ea.EmployeeId == req.Id, ct);

            if (employeeAddress != null)
            {
                employeeAddress.ProvinceId = provinceId;
                employeeAddress.WardId = wardId;
            }
            else
            {
                _context.EmployeeAddresses.Add(new EmployeeAddress
                {
                    EmployeeId = req.Id,
                    ProvinceId = provinceId,
                    WardId = wardId
                });
            }
        }

        await _context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}