using BackEnd.Data;
using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.AccountEndpoints;

public record DeleteAccountRequest(int Id);

public class Delete(ApplicationDbContext context) : Endpoint<DeleteAccountRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(DeleteAccountRequest req, CancellationToken ct)
    {
        var account = await _context.Accounts.FindAsync(new object[] { req.Id }, ct);

        if (account is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}