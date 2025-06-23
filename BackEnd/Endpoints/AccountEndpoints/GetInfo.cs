using BackEnd.Data;
using BackEnd.Endpoints.AccountEndpoints.DTOs;
using BackEnd.Endpoints.AccountEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackEnd.Endpoints.AccountEndpoints;

public class GetInfo(ApplicationDbContext context) : EndpointWithoutRequest<GetInfoDTO>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("info");
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var accountId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException());

        var account = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountId == accountId, ct);

        if (account is null)
        {
            ThrowError("Account not found");
            return;
        }

        await SendAsync(account.ToGetInfoDTO(), cancellation: ct);
    }
}