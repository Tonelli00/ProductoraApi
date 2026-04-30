using Application.DTOs.Users;
using Application.Interfaces.Users;
using BCrypt.Net;
using Domain.Entities;
using Domain.Exceptions;
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
                throw new ArgumentException("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(command.Email))
                throw new ArgumentException("El email es requerido");

            var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
            if (existingUser != null){
                throw new EmailConflictException("El email ya existe, ingrese otra dirección de email");
            }

            var user = new Domain.Entities.User
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash=BCrypt.Net.BCrypt.HashPassword(command.PasswordHash)
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
