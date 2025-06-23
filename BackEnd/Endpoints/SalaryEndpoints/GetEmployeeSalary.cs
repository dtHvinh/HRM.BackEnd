using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Endpoints.SalaryEndpoints.DTOs;
using BackEnd.Endpoints.SalaryEndpoints.Mapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.SalaryEndpoints;

public record GetEmployeeSalaryRequest(int EmployeeId);

public class GetEmployeeSalary(ApplicationDbContext context) : Endpoint<GetEmployeeSalaryRequest, List<GetSalaryDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{employeeId}");
        Group<SalaryGroup>();
    }

    public override async Task HandleAsync(GetEmployeeSalaryRequest req, CancellationToken ct)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeSalaries!.OrderByDescending(e => e.PaymentDate))
            .FirstOrDefaultAsync(e => e.EmployeeId == req.EmployeeId, ct);

        if (employee == null || employee.EmployeeSalaries is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var res = employee.EmployeeSalaries.Select(es => es.Salary!.ToGetSalaryDTO(es.PaymentDate.ToString())).ToList();

        await SendOkAsync(res, ct);
    }
}