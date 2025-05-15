using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Extensions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Endpoints.AuthEndpoints;

public record LoginRequest(string Username, string Password);

public record LoginResponse(string AccessToken);

public class Login(ApplicationDbContext dbContext) : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ApplicationDbContext _context = dbContext;

    public override void Configure()
    {
        Post("login");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Username == req.Username && a.Password == req.Password, ct);

        if (account == null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            var token = Config["SecretKey"]!.GenerateToken();
            var response = new LoginResponse(token);
            await SendOkAsync(response, cancellation: ct);
        }
    }
}