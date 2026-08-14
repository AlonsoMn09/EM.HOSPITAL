using EM.Hospital.Application.Common.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
