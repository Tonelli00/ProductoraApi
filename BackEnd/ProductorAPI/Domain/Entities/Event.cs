

namespace Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Venue { get; set; }
        public string Status { get; set; }

        public IEnumerable<Sector> Sectors { get; set; }
    }
}
