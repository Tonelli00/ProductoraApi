using Application.Interfaces.AuditLogs;
using Application.Interfaces.Events;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.Interfaces.Users;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Reservation;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Commands.User;
using Application.UseCase.Handlers.AuditLogs;
using Application.UseCase.Queries.Events;
using Application.UseCase.Queries.Reservations;
using Application.UseCase.Queries.Seats;
using Application.UseCase.Queries.Users;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Productora.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a la DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));



//Inyecciones

//EVENT
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetPagedEventsQueryHandler, GetPagedEventsQueryHandler>();
builder.Services.AddScoped<IGetEventByIdHandler,GetEventByIdHandler>();
builder.Services.AddScoped<IGetSeatsByEventIdHandler,GetSeatsByEventIdHandler>();


//SEAT

builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IGetSeatsBySectorIdQueryHandler, GetSeatsBySectorIdHandler>();
builder.Services.AddScoped<IMarkSeatAsReservedCommandHandler, MarkSeatAsReservedHandler>();
builder.Services.AddScoped<IGetReservedSeatsByEventHandler, GetReservedSeatsByEventHandler>();
builder.Services.AddScoped<IGetSeatByIdHandler, GetSeatByIdHandler>();

//SECTOR

// RESERVATION   
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICreateReservationCommandHandler, CreateReservationHandler>();
builder.Services.AddScoped<IGetReservationsByUserQueryHandler, GetReservationsByUserHandler>();
builder.Services.AddScoped<IGetReservationByIdQueryHandler, GetReservationByIdHandler>();

// USER
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
builder.Services.AddScoped<ICreateUserCommandHandler, CreateUserHandler>();

//AuditLog
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<ICreateAuditLogCommandHanlder, CreateAuditLogHandler>();
builder.Services.AddScoped<IGetAuditLogsByUserQueryHandler, GetAuditLogByUserHandler>();

//CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFront", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFront");

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
