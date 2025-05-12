using BackEnd.Endpoints.Groups;
using FastEndpoints;

namespace BackEnd.Endpoints.AuthEndpoints;

public record LoginRequest(string Username, string Password);

public record LoginResponse();

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("login");
        Group<AuthGroup>();
    }

    public override async Task<LoginResponse> ExecuteAsync(LoginRequest req, CancellationToken ct)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
        };

        HttpContext.Response.Cookies.Append("token", "mytoken", cookieOptions);

        return await Task.FromResult<LoginResponse>(new());
    }
}