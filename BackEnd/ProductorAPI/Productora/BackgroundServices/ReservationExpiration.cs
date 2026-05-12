using Application.Interfaces.Reservations;
using Application.Interfaces.Seats;
using Application.UseCase.Queries.Seats;

namespace Productora.BackgroundServices
{
    public class ReservationExpiration:BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);
        public ReservationExpiration(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                using var scope = _serviceProvider.CreateScope();

                var getExpiredReservationsHandler = scope.ServiceProvider.GetRequiredService<IGetExpiredReservationsHandler>();
                var getSeatByIdHandler = scope.ServiceProvider.GetRequiredService<IGetSeatByIdHandler>();

                var reservations = await getExpiredReservationsHandler.Handler();

                foreach (var reservation in reservations)
                {
                    reservation.Status = "Expired";
                    var seat = await getSeatByIdHandler.Handle(new GetSeatByIdQuery { SeatId=reservation.SeatId});
                    seat.Status = "Available";

                    // Acá tendría que ir el metodo cancelReservatión
                }

                await Task.Delay(_interval, stoppingToken);
                
            }
                
        }

    }
}
