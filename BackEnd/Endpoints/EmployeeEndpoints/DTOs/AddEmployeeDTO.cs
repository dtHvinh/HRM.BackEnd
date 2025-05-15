namespace BackEnd.Endpoints.EmployeeEndpoints.DTOs;

public record AddEmployeeDTO
{
    public required string FullName { get; init; }
    public required string DateOfBirth { get; init; }
    public required string Gender { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Department { get; init; }
    public required string Position { get; init; }
    public required string ProvinceId { get; init; }
    public required string WardId { get; init; }
}