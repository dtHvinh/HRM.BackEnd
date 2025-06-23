using BackEnd.Data;
using BackEnd.Endpoints.EmployeeEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.EmployeeEndpoints;

public record GetEmployeeReportRequest(int EmployeeId);

public record GetEmployeeReportResponse(int EmployeeId, string FullName, DateTimeOffset DOB, string Gender, string Email, string Phone, string Department, string Position, string Province, string Ward, decimal SalaryCoefficient, decimal InsuranceCoefficient, decimal AllowanceCoefficient, DateOnly SalaryRaiseDay);

public class GetEmployeeWithSalaryAndBenefit(ApplicationDbContext context) : Endpoint<GetEmployeeReportRequest, GetEmployeeReportResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("/{employeeId}/report");
        AllowAnonymous();
        Group<EmployeeGroup>();
    }

    public override async Task HandleAsync(GetEmployeeReportRequest req, CancellationToken ct)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeDepartments)
            .Include(e => e.EmployeeSalaries)
            .Include(e => e.EmployeeBenefit)
            .Include(e => e.EmployeeAddress)
            .Where(e => e.EmployeeId == req.EmployeeId)
            .FirstOrDefaultAsync(ct);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var latestDepartment = employee.EmployeeDepartments?
            .OrderByDescending(e => e.AppointmentDate)
            .FirstOrDefault();

        var positionName = latestDepartment?.Position?.Name ?? "N/A";

        var latestDepartmentName = latestDepartment?.Department?.Name ?? "N/A";

        var provinceName = employee.EmployeeAddress?.Province?.ProvinceName ?? "N/A";
        var wardName = employee.EmployeeAddress?.Ward?.WardName ?? "N/A";

        var latestSalary = employee.EmployeeSalaries?
            .OrderByDescending(e => e.PaymentDate)
            .FirstOrDefault();

        var paymentDate = latestSalary?.PaymentDate ?? DateOnly.MinValue;
        var salaryRaiseDate = paymentDate.AddYears(3);

        var salaryCoefficient = latestSalary?.Salary?.SalaryCoefficient ?? 0;

        var insuranceCoefficient = employee.EmployeeBenefit?.Insurance?.InsuranceCoefficient ?? 0;
        var allowanceCoefficient = employee.EmployeeBenefit?.Allowance?.AllowanceCoefficient ?? 0;

        var res = employee.ToGetDetailWithSalaryResponse(
              latestDepartmentName,
              positionName,
              provinceName,
              wardName,
              salaryCoefficient,
              insuranceCoefficient,
              allowanceCoefficient,
              salaryRaiseDate);

        await SendOkAsync(res, ct);
    }
}