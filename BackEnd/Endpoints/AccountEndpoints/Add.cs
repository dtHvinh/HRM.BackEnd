using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.AccountEndpoints;

public record AddAccountRequest(string Username, string Password, bool IsAdmin);

public class Add(ApplicationDbContext context) : Endpoint<AddAccountRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(AddAccountRequest req, CancellationToken ct)
    {
        var account = new Account
        {
            Username = req.Username,
            Password = req.Password,
            IsAdmin = req.IsAdmin
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(new { Id = account.AccountId }, ct);
    }
}