namespace BackEnd.Endpoints.AllowanceEndpoints.DTOs;

public class GetAllowanceDTO
{
    public int AllowanceId { get; set; }
    public string AllowanceName { get; set; } = null!;
    public decimal AllowanceCoefficient { get; set; }
}