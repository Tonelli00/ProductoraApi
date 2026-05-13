using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.Seat
{
    public class MarkSeatAtAvailableCommand
    {
        public Domain.Entities.Seat Seat { get; set; }
    }
}
