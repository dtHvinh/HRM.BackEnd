namespace BackEnd.Endpoints.AccountEndpoints.DTOs;

public record GetInfoDTO(int AccountId, string Username, string Password, bool IsAdmin);

