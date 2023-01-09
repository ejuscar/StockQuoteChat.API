using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockQuoteChat.API;
using StockQuoteChat.API.Hubs;
using StockQuoteChat.API.Models;
using StockQuoteChat.Infrastructure;
using StockQuoteChat.Infrastructure.Repositories;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;
using System.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Save the connectionId for the user
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// DbContext
builder.Services.AddDbContext<ChatDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//IoC
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
