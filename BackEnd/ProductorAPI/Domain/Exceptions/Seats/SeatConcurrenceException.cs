using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Seats
{
    public class SeatConcurrenceException : Exception
    {
        public SeatConcurrenceException(string message) : base(message) { }
    }
}
