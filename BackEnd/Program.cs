using BackEnd.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

builder.Services
    .AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(builder.Configuration.GetConnectionString("Default")); });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigins",
                      policy =>
                      {
                          policy.WithOrigins(
                              "http://localhost:3000");
                          policy.SetIsOriginAllowed(origin => true);
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

//builder.Services
//    .AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//    {
//        options.TokenValidationParameters = new()
//        {
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]!)),
//            ClockSkew = TimeSpan.Zero
//        };
//    });

//builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultExceptionHandler().UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
app.UseHttpsRedirection();
app.UseCors("AllowOrigins");
app.Run();
