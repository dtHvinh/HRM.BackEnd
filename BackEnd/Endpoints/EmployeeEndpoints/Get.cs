using BackEnd.Data;
using BackEnd.Endpoints.EmployeeEndpoints.DTOs;
using BackEnd.Endpoints.EmployeeEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
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
        var departmentFilter = Query<int>("dep", false);
        var genderFilter = Query<int>("gen", false);
        var provinceFilter = Query<int>("province", false);
        var wardFilter = Query<int>("ward", false);

        var query = _context.Employees
            .Include(e => e.EmployeeDepartments)
            .Include(e => e.EmployeeAddress)
            .Where(e => true);

        if (departmentFilter != 0)
            query = query
                 .Where(e => e.EmployeeDepartments!
                     .OrderByDescending(e => e.AppointmentDate).First()!.Department!.DepartmentId == departmentFilter);

        if (genderFilter != 0)
            query = query.Where(e => e.Gender == Enum.GetValues<Gender>()[genderFilter - 1]);

        if (provinceFilter != 0)
            query = query.Where(e => e.EmployeeAddress!.Province!.ProvinceId == provinceFilter);

        if (wardFilter != 0)
            query = query.Where(e => e.EmployeeAddress!.Ward!.WardId == wardFilter);

        var result = await query.Select(e => e.ToGetEmployeeDTO(
                e.EmployeeDepartments!.OrderByDescending(e => e.AppointmentDate).First()!.Department!.Name,
                e.EmployeeDepartments!.OrderByDescending(e => e.AppointmentDate).First()!.Position!.Name,
                e.EmployeeAddress!.Province!.ProvinceName,
                e.EmployeeAddress!.Ward!.WardName)).ToListAsync(ct);

        await SendAsync(result, cancellation: ct);
    }
}