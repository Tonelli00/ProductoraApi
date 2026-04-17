using Domain.Entities;

namespace Infrastructure.Persistence.DbSeeder
{
    public static class SeatSeed
    {
        public static List<Seat> Generate(int sectorStartId = 1, int sectorCount = 2)
        {
            var seats = new List<Seat>();

            var rows = new[] { "A", "B", "C", "D", "E" };
            int seatsPerSector = 50;

            for (int sectorId = sectorStartId; sectorId < sectorStartId + sectorCount; sectorId++)
            {
                for (int i = 1; i <= seatsPerSector; i++)
                {
                    var row = rows[(i - 1) / 10];

                    seats.Add(new Seat
                    {
                        Id = Guid.NewGuid(),
                        SectorId = sectorId,
                        SeatNumber = i,
                        RowIdentifier = row,
                        Status = "Available",
                        Version = 1
                    });
                }
            }

            return seats;
        }
    }
}