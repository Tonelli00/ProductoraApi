using Application.DTOs.Users;
using Application.UseCase.Commands.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Users
{
    public interface ILoginUserHandler
    {
        Task<UserResponse> Handle(LoginUserCommand command);

    }
}
