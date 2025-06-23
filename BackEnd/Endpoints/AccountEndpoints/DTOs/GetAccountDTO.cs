namespace BackEnd.Endpoints.AccountEndpoints.DTOs;

public record GetAccountDTO(int AccountId, string Username, string Password, bool IsAdmin);
