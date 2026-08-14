using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Services.Auth;
using EM.Hospital.Application.Features.Auth.Login.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Auth.Login
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, Result<LoginResponse>>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public LoginQueryHandler(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        public async Task<Result<LoginResponse>> HandleAsync(LoginQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _userService.FindByUserNameAsync(query.userName);
            if (user.IsFailure)
                return Result.Failure<LoginResponse>(user.Message!);

            var password = await _userService.CheckPasswordAsync(user.Value, query.Password);
            if (!password)
                return Result.Failure<LoginResponse>("Invalid password");

            var token = _authService.GenerateToken(user.Value);
            return Result.Success(new LoginResponse
            {
                FullName = user.Value!.CustomerFullName,
                Role = user.Value.Role,
                AccessToken = token
            });
        }
    }
}
