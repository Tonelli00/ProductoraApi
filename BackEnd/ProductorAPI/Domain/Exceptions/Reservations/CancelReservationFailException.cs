
namespace Domain.Exceptions.Reservations
{
    public class CancelReservationFailException : Exception
    {
        public CancelReservationFailException(string message) : base(message) { }
    }
}
