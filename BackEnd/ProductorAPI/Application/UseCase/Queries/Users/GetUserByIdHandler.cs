using Application.DTOs.Users;
using Application.Interfaces.Users;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.Users
{
    public class GetUserByIdHandler : IGetUserByIdHandler
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handler(GetUserByIdQuery query)
        {
            if(query.UserId<0 || query.UserId == 0) 
            {
                throw new ArgumentException("Ingrese valores válidos");
            }

            var user = await _userRepository.GetUserById(query.UserId);
            if (user == null) 
            {
                throw new UserNotFoundException("Usuario no encontrado");
            }
                
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
