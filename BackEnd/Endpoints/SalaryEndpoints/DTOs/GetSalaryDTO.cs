namespace BackEnd.Endpoints.SalaryEndpoints.DTOs;

public class GetSalaryDTO
{
    public int SalaryId { get; set; }
    public decimal SalaryCoefficient { get; set; }
    public string PaymentDate { get; set; }
}