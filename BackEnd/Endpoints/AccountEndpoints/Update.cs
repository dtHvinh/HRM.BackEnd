using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using BackEnd.Models;
using FastEndpoints;

namespace BackEnd.Endpoints.AccountEndpoints;

public record UpdateAccountRequest(int Id, string Username, string Password);

public class Update(ApplicationDbContext context) : Endpoint<UpdateAccountRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{id}");
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(UpdateAccountRequest req, CancellationToken ct)
    {
        var account = await _context.Accounts.FindAsync(new object[] { req.Id }, ct);

        if (account is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        account.Username = req.Username;
        account.Password = req.Password;
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}