using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCrudPessoa.Data;
using ProjetoCrudPessoa.DTOs;
using ProjetoCrudPessoa.Repositories;
using ProjetoCrudPessoa.Services;
using ProjetoCrudPessoa.Validators;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new ErrorResponseDto
            {
                Mensagem = "Erro de validação.",
                Erros = erros
            };

            return new BadRequestObjectResult(response);
        };
    });

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repository Pattern
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

// Service Layer
builder.Services.AddScoped<IPessoaService, PessoaService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PessoaCreateValidator>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();