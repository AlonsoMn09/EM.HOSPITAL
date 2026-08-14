using EM.Hospital.Application.Common.Models.Auth;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.Services.Auth
{
    public interface IUserService
    {
        Task<Result> CreateAsync(User user);
        Task<Result<User>> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
