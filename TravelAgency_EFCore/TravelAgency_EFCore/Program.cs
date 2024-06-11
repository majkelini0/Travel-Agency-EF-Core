using Microsoft.EntityFrameworkCore;
using TravelAgency_EFCore.Context;
using TravelAgency_EFCore.Repositories;
using TravelAgency_EFCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IClientTripRepository, ClientTripRepository>();
builder.Services.AddScoped<IClientTripService, ClientTripService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApbdEfcContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();