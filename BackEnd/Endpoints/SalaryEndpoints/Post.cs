using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.SalaryEndpoints;

public record CreateSalaryDTO(int EmployeeId, decimal SalaryCoefficient, string? PaymentDate);

public class Post(ApplicationDbContext context) : Endpoint<CreateSalaryDTO>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{employeeId}");
        Group<SalaryGroup>();
    }

    public override async Task HandleAsync(CreateSalaryDTO req, CancellationToken ct)
    {
        var salary = new Salary
        {
            SalaryCoefficient = req.SalaryCoefficient,
        };

        _context.Salaries.Add(salary);
        await _context.SaveChangesAsync();

        var emplSalary = new EmployeeSalary
        {
            EmployeeId = req.EmployeeId,
            PaymentDate = !string.IsNullOrEmpty(req.PaymentDate)
            ? DateOnly.FromDateTime(DateTime.Parse(req.PaymentDate))
            : DateOnly.FromDateTime(DateTime.UtcNow),
            SalaryId = salary.SalaryId,
        };

        _context.EmployeeSalaries.Add(emplSalary);

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(cancellation: ct);
    }
}
