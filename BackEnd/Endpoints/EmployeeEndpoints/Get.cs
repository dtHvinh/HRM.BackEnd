using BackEnd.Data;
using BackEnd.Endpoints.EmployeeEndpoints.DTOs;
using BackEnd.Endpoints.EmployeeEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetEmployeeDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var employeesWithRelations = await _context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(e => new
            {
                Employee = e,
                Department = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.EmployeeId)
                    .OrderByDescending(ed => ed.AppointmentDate)
                    .Join(_context.Departments,
                        ed => ed.DepartmentId,
                        d => d.DepartmentId,
                        (ed, d) => d.Name)
                    .FirstOrDefault() ?? "",
                Position = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.EmployeeId)
                    .OrderByDescending(ed => ed.AppointmentDate)
                    .Join(_context.Positions,
                        ed => ed.PositionId,
                        p => p.PositionId,
                        (ed, p) => p.Name)
                    .FirstOrDefault() ?? "",
                Province = _context.EmployeeAddresses
                    .Where(ea => ea.EmployeeId == e.EmployeeId)
                    .Join(_context.Province,
                        ea => ea.ProvinceId,
                        p => p.ProvinceId,
                        (ea, p) => p.ProvinceName)
                    .FirstOrDefault() ?? "",
                Ward = _context.EmployeeAddresses
                    .Where(ea => ea.EmployeeId == e.EmployeeId)
                    .Join(_context.Wards,
                        ea => ea.WardId,
                        w => w.WardId,
                        (ea, w) => w.WardName)
                    .FirstOrDefault() ?? ""
            })
            .ToListAsync(ct);

        var result = employeesWithRelations.Select(e => e.Employee.ToGetEmployeeDTO(
            e.Department,
            e.Position,
            e.Province,
            e.Ward
        )).ToList();


        await SendAsync(result, cancellation: ct);
    }
}