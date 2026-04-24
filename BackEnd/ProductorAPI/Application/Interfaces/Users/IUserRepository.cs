using Application.UseCase.Commands.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task Handler(User user);
    }
}
