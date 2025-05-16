using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record UpdateEmployeeRequest(int Id, string FullName, string Dob, string Gender, string Email, string Phone, string Department, string Position, string ProvinceId, string WardId);

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
        if (!string.IsNullOrEmpty(req.FullName))
            employee.FullName = req.FullName;

        if (!string.IsNullOrEmpty(req.Dob))
            employee.DOB = DateTimeOffset.Parse(req.Dob);

        if (!string.IsNullOrEmpty(req.Gender))
            employee.Gender = Enum.Parse<Gender>(req.Gender);

        if (!string.IsNullOrEmpty(req.Email))
            employee.Email = req.Email;

        if (!string.IsNullOrEmpty(req.Phone))
            employee.Phone = req.Phone;

        // Update department relationship
        if (int.TryParse(req.Department, out int departmentId) && departmentId != default)
        {
            var employeeDepartment = await _context.EmployeeDepartments
                .FirstOrDefaultAsync(ed => ed.EmployeeId == req.Id, ct);

            _context.EmployeeDepartments.Add(new EmployeeDepartment
            {
                EmployeeId = req.Id,
                DepartmentId = departmentId
            });
        }

        // Update address information
        if (int.TryParse(req.ProvinceId, out int provinceId) && int.TryParse(req.WardId, out int wardId)
            && provinceId != default && wardId != default)
        {
            var employeeAddress = await _context.EmployeeAddresses
                .FirstOrDefaultAsync(ea => ea.EmployeeId == req.Id, ct);

            if (employeeAddress != null)
            {
                _context.EmployeeAddresses.Remove(employeeAddress);
            }

            _context.EmployeeAddresses.Add(new EmployeeAddress
            {
                EmployeeId = req.Id,
                ProvinceId = provinceId,
                WardId = wardId
            });
        }

        await _context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}