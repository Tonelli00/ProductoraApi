using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.User
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
