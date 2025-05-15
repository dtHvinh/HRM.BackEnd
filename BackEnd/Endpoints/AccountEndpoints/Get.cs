using BackEnd.Data;
using BackEnd.Endpoints.AccountEndpoints.DTOs;
using BackEnd.Endpoints.AccountEndpoints.Mapper;
using BackEnd.Endpoints.Groups;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Endpoints.AccountEndpoints;

public class Get(ApplicationDbContext context) : EndpointWithoutRequest<List<GetAccountDTO>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<AccountGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var accounts = await _context.Accounts.Select(e => e.ToGetAccountDTO()).ToListAsync(ct);
        await SendAsync(accounts, cancellation: ct);
    }
}