using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Auth.Login.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Auth.Login
{
    public record LoginQuery(string userName, string Password) : IQuery<Result<LoginResponse>>;
}
