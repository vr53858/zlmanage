using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ZLManage.ApplicationServices.Services.Administrator;
using ZLManage.ApplicationServices.Services.Let;
using ZLManage.ApplicationServices.Services.Pista;
using ZLManage.ApplicationServices.Services.Zaposlenik;
using ZLManage.ApplicationServices.Services.Zrakoplov;
using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Validation.Validators;
using ZLManage.DomainServices.Interfaces;
using ZLManage.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ZLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IValidator<Zrakoplov>, ZrakoplovValidator>();

builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<ILetService, LetService>();
builder.Services.AddScoped<IPistaService, PistaService>();
builder.Services.AddScoped<IZaposlenikService, ZaposlenikService>();
builder.Services.AddScoped<IZrakoplovService, ZrakoplovService>();

builder.Services.AddScoped<ILetRepository, LetRepository>();
builder.Services.AddScoped<IPistaRepository, PistaRepository>();
builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IZaposlenikRepository, ZaposlenikRepository>();
builder.Services.AddScoped<IZrakoplovRepository, ZrakoplovRepository>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5174") // Or your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod();
        policy.WithOrigins("http://localhost:5173") // For HTTPS
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
