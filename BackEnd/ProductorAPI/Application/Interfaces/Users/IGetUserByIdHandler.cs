using Application.DTOs.Users;
using Application.UseCase.Queries.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Users
{
    public interface IGetUserByIdHandler
    {
        Task<UserResponse?> Handler(GetUserByIdQuery query);
    }
}
