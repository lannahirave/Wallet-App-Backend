using Microsoft.EntityFrameworkCore;
using WAB.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WabContext>(option =>
{
    option.EnableSensitiveDataLogging();
    option.UseNpgsql(builder.Configuration.GetConnectionString("WalletDB"));
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

app.MapControllers();

app.Run();