using Application.Interfaces.Events;
using Application.Interfaces.Seats;
using Application.UseCase.Handlers.Events;
using Application.UseCase.Handlers.Seats;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Productora.Middleware;

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
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetAllEventsQueryHandler,GetAllEventsQueryHandler>();
builder.Services.AddScoped<IGetPagedEventsQueryHandler, GetPagedEventsQueryHandler>();


//SEAT

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IGetSeatsBySectorIdQueryHandler, GetSeatsBySectorIdHandler>();
builder.Services.AddScoped<IMarkSeatAsReservedCommandHandler, MarkSeatAsReservedHandler>();
builder.Services.AddScoped<IGetReservedSeatsByEventHandler, GetReservedSeatsByEventHandler>();

//SECTOR




var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
