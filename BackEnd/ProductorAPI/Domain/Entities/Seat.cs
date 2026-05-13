

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Seat
    {
        public Guid Id { get; set; }
        public int SectorId { get; set; }
        public string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }
       
        public Sector Sector { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

    }
}
