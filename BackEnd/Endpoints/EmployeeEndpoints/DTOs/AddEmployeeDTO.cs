namespace BackEnd.Endpoints.EmployeeEndpoints.DTOs;

public record AddEmployeeDTO(string FullName, string Dob, string Gender, string Email, string Phone, string DepartmentId, string PositionId, string ProvinceId, string WardId);
