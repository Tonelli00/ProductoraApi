    using Domain.Entities;

    namespace Application.Interfaces.Seats
    {
        public interface ISeatRepository
        {
            Task InsertSeatsAsync(List<Seat>Seats,CancellationToken ct=default);
            Task<IEnumerable<Seat>> GetSeatsBySectorId(int SectorId,CancellationToken ct=default);
            Task<Seat> GetSeatBySeatNumberAndSectorId(int SeatNumber,int SectorId,CancellationToken ct = default);
            Task<IEnumerable<Seat>> GetReservedSeatsByEventId(int EventId,CancellationToken ct = default);
            Task<Seat> GetSeatById(Guid SeatId,CancellationToken ct=default);
            Task UpdateSeatStatus(Seat seat,CancellationToken ct = default);

            Task<IEnumerable<Seat>> GetSeatsByEventId(int EventId, CancellationToken ct = default);

        }
    }
