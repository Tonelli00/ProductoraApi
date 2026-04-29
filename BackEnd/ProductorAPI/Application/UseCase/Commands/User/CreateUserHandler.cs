using Application.DTOs.Users;
using Application.Interfaces.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Commands.User
{
    public class CreateUserHandler : ICreateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handler(CreateUserCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Name))
                throw new Exception("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new Exception("El email es requerido");

            var user = new Domain.Entities.User
            {
                Name = command.Name,
                Email = command.Email,
            };

            await _userRepository.InsertAsync(user);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
