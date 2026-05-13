using Application.Interfaces.AuditLogs;
using Application.Interfaces.Events;
using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.Interfaces.Sectors;
using Application.Interfaces.Users;
using Application.UseCase.Commands.AuditLog;
using Application.UseCase.Commands.Reservation;
using Application.UseCase.Commands.Seat;
using Application.UseCase.Commands.User;
using Application.UseCase.Handlers.AuditLogs;
using Application.UseCase.Commands.Sector;
using Application.UseCase.Queries.Events;
using Application.UseCase.Queries.Reservations;
using Application.UseCase.Queries.Seats;
using Application.UseCase.Queries.Sectors;
using Application.UseCase.Queries.Users;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Application.Interfaces;
using Infrastructure.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Conexión a la DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//Inyección del UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



//Configuración para que la urls sean minusculas
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;        
    options.LowercaseQueryStrings = true; 
});


//EVENT
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IGetPagedEventsHandler, GetPagedEventsHandler>();
builder.Services.AddScoped<IGetEventByIdHandler,GetEventByIdHandler>();
builder.Services.AddScoped<IGetSeatsByEventIdHandler,GetSeatsByEventIdHandler>();


//SEAT
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IGetSeatsBySectorIdHandler, GetSeatsBySectorIdHandler>();
builder.Services.AddScoped<IMarkSeatAsReservedHandler, MarkSeatAsReservedHandler>();
builder.Services.AddScoped<IGetReservedSeatsByEventHandler, GetReservedSeatsByEventHandler>();
builder.Services.AddScoped<IGetSeatByIdHandler, GetSeatByIdHandler>();
builder.Services.AddScoped<ICreateSeatsHandler, CreateSeatsHandler>();

//SECTOR
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ICreateSectorHandler, CreateSectorHandler>();
builder.Services.AddScoped<IGetSectorByIdHandler, GetSectorByIdHandler>();

// RESERVATION   
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICreateReservationCommandHandler, CreateReservationHandler>();
builder.Services.AddScoped<IGetReservationsByUserQueryHandler, GetReservationsByUserHandler>();
builder.Services.AddScoped<IGetReservationByIdQueryHandler, GetReservationByIdHandler>();
builder.Services.AddScoped<IGetExpiredReservationsHandler, GetExpiredReservationsHandler>();
builder.Services.AddScoped<IConfirmReservationHandler, ConfirmReservationHandler>();

// USER
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGetUserByIdHandler, GetUserByIdHandler>();
builder.Services.AddScoped<ICreateUserCommandHandler, CreateUserHandler>();
builder.Services.AddScoped<ILoginUserHandler, LoginUserHandler>();

//AuditLog
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<ICreateAuditLogCommandHanlder, CreateAuditLogHandler>();
builder.Services.AddScoped<IGetAuditLogsByUserQueryHandler, GetAuditLogByUserHandler>();

//SWAGGER EXAMPLES
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Productora",
        Version = "v1"
    });
});


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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Productora API v1");
    });
}
app.UseCors("AllowFront");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
