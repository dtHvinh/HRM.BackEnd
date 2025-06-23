namespace BackEnd.Endpoints.EmployeeBenefitEndpoints.DTOs;

public class GetEmployeeBenefitDTO
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = null!;
    public int AllowanceId { get; set; }
    public string AllowanceName { get; set; } = null!;
    public decimal AllowanceCoefficient { get; set; }
    public int InsuranceId { get; set; }
    public string InsuranceName { get; set; } = null!;
    public decimal InsuranceCoefficient { get; set; }
    public DateTimeOffset JoinedDate { get; set; }
}