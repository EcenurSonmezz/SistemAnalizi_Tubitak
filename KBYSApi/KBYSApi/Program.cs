using FluentValidation.AspNetCore;
using KBYS.BusinessLogic.Validation.Users;
using KBYS.DataAcces.KBYSDbContexts;
using KBYSApi.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KBYSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ConfigureServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // Döngüsel referanslarý önlemek için
        options.JsonSerializerOptions.MaxDepth = 64; // Derinlik sýnýrýný artýrabilirsiniz
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddUserCommandValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithHeaders("Content-Type", "Authorization"); // Add the necessary headers here
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins"); // Use the CORS policy

app.MapControllers();

app.Run();
