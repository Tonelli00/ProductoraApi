using Application.DTOs.Users;
using Application.Interfaces.Users;
using Domain.Exceptions;
using Domain.Exceptions.Users;

namespace Application.UseCase.Commands.User
{
    public class LoginUserHandler : ILoginUserHandler
    {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(LoginUserCommand command)
        {
            var email = command.Email.Trim().ToLower();
            var _user = await _userRepository.GetUserByEmailAsync(email);
           if(_user == null) 
           {
                throw new UserCredentialsIncorrectException("El usuario no fue encontrado");
           }

            bool isValidPsw= BCrypt.Net.BCrypt.Verify(command.Password,_user.PasswordHash);

            if (!isValidPsw)
            {
                throw new PasswordConflictException("La contraseña ingresada no es correcta.");
            }
            
            return new UserResponse { 
                Id = _user.Id,
                Name=_user.Name,
                Email = _user.Email };
          
        }
    }
}
