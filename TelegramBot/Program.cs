using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.PortableExecutable;
using TelegramBot.Data;
using TelegramBot.Data.Repositories;
using TelegramBot.Domain.Repositories;
using TelegramInteraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<BotHostedService>();
builder.Services.AddHttpClient<CatApi>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICountRepository, CountRepository>();

var connectionString = builder.Configuration.GetConnectionString("TelegramDB");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

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
