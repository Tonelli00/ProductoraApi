using Application.DTOs.Users;
using Application.Interfaces.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.Users
{
    public class GetUserByIdQueryHandler : IGetUserByIdQueryHandler
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handler(GetUserByIdQuery query)
        {
            var user = await _userRepository.GetUserById(query.UserId);
            if (user == null) return null;
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
