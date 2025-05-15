namespace BackEnd.Endpoints.EmployeeEndpoints.DTOs;

public record GetEmployeeDTO(int EmployeeId, string FullName, DateTimeOffset DOB, string Gender, string Email, string Phone, string Department, string Position, string Province, string Ward);