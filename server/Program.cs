using JobPortal.Application.Interfaces;
using JobPortal.Application.Services;
using JobPortal.Data.Context;
using JobPortal.Data.Repositories;
using JobPortal.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BaseContext>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobAdRepository, JobAdRepository>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "715151845153-q5u9l13j6tu5ghqimerorj17hegcgvts.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
});
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddScoped<IJobAdRepository, JobAdRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
