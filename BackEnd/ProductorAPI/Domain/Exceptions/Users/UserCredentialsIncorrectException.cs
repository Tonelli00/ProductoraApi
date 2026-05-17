using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Users
{
    public class UserCredentialsIncorrectException : Exception
    {
        public UserCredentialsIncorrectException(string message) : base(message) { }
    }
}
