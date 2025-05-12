using BackEnd.Data;
using BackEnd.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigins",
                      policy =>
                      {
                          policy.WithOrigins(
                              "https://localhost:7052",
                              "http://192.168.1.3",
                              "https://192.168.1.3",
                              "http://localhost:7051",
                              "https://qa-web-mu.vercel.app",
                              "https://qa-web-dthvinhs-projects.vercel.app",
                              "https://qa-2vvl99o65-dthvinhs-projects.vercel.app");
                          policy.AllowAnyHeader();
                          policy.SetIsOriginAllowed(origin => true);
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseEndpoints();
app.UseCors("AllowOrigins");
app.Run();
