using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.UseCase.Commands.Reservation;
using Application.UseCase.Queries.Seats;
using Domain.Entities;

namespace Productora.BackgroundServices
{
    public class ReservationExpiration:BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);
        public ReservationExpiration(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                using var scope = _serviceProvider.CreateScope();

                var cancelReservationHanlder = scope.ServiceProvider.GetRequiredService<ICancelReservationHandler>();
                var getExpiredReservationsHandler = scope.ServiceProvider.GetRequiredService<IGetExpiredReservationsHandler>();
                var getSeatByIdHandler = scope.ServiceProvider.GetRequiredService<IGetSeatByIdHandler>();

                var reservations = await getExpiredReservationsHandler.Handler();
                if(reservations == null || !reservations.Any())
                {
                    Console.WriteLine($"[{DateTime.UtcNow}] No hay reservas expiradas en este momento.");
                    await Task.Delay(_interval, stoppingToken);
                    continue;
                }
                foreach (var reservation in reservations)
                {
                    // cancelamos las reservas expiradas
                    await cancelReservationHanlder.Handler(new CancelReservationCommand { ReservationId = reservation.Id });
                    Console.WriteLine($"[{DateTime.UtcNow}] Ejecutando limpieza de órdenes expiradas...");
                }

                await Task.Delay(_interval, stoppingToken);
                
            }
                
        }

    }
}
