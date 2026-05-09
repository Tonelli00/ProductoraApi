
namespace Domain.Exceptions
{
    public class PasswordConflictException:Exception
    {
        public PasswordConflictException(string message) : base(message) { }
        
    }
}
