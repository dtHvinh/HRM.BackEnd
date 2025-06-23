namespace BackEnd.Endpoints.InsuranceEndpoints.DTOs;

public class GetInsuranceDTO
{
    public int InsuranceId { get; set; }
    public string InsuranceName { get; set; } = null!;
    public decimal InsuranceCoefficient { get; set; }
}