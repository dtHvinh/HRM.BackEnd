using BackEnd.Data;
using BackEnd.Endpoints.EmployeeEndpoints.DTOs;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public class Add(ApplicationDbContext context) : Endpoint<AddEmployeeDTO>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(AddEmployeeDTO req, CancellationToken ct)
    {
        DateTimeOffset dob = DateTimeOffset.Parse(req.DateOfBirth);

        Gender gender = Enum.Parse<Gender>(req.Gender);

        var employee = new Employee
        {
            FullName = req.FullName,
            DOB = dob,
            Gender = gender,
            Email = req.Email,
            Phone = req.Phone
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(ct);

        if (int.TryParse(req.Department, out int departmentId))
        {
            var employeeDepartment = new EmployeeDepartment
            {
                EmployeeId = employee.EmployeeId,
                DepartmentId = departmentId,
                PositionId = int.Parse(req.Position),
                TransferInDate = DateTimeOffset.UtcNow,
                AppointmentDate = DateTimeOffset.UtcNow
            };
            _context.EmployeeDepartments.Add(employeeDepartment);
        }

        if (int.TryParse(req.ProvinceId, out int provinceId) && int.TryParse(req.WardId, out int wardId))
        {
            var employeeAddress = new EmployeeAddress
            {
                EmployeeId = employee.EmployeeId,
                ProvinceId = provinceId,
                WardId = wardId
            };
            _context.EmployeeAddresses.Add(employeeAddress);
        }

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(new { Id = employee.EmployeeId }, ct);
    }
}