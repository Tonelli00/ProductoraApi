using Application.Interfaces;
using Application.UseCase.Event;
using Application.UseCase.Seat;
using Application.UseCase.Sector;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Persistence.DbSeeder;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Custom

//Inyecto el bdContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));



//Inyecciones

//EVENT
builder.Services.AddScoped<IEventServices,EventService>();
builder.Services.AddScoped<IEventQuery, EventQuery>();
builder.Services.AddScoped<IEventCommand, EventCommand>();

//SEAT
builder.Services.AddScoped<ISeatServices, SeatService>();
builder.Services.AddScoped<ISeatQuery, SeatQuery>();
builder.Services.AddScoped<ISeatCommand, SeatCommand>();

//SECTOR
builder.Services.AddScoped<ISectorServices, SectorService>();
builder.Services.AddScoped<ISectorQuery, SectorQuery>();
builder.Services.AddScoped<ISectorCommand, SectorCommand>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Seats.Any())
    {
        var seats = SeatSeed.Generate(1, 2); 
        context.Seats.AddRange(seats);
        context.SaveChanges();
    }
}



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
