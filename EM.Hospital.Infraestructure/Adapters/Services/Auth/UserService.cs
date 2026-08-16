using EM.Hospital.Application.Common.Contracts.Services.Auth;
using EM.Hospital.Application.Common.Models.Auth;
using EM.Hospital.Domain.Common;
using EM.Hospital.Infraestructure.Configuration.Authentication.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Adapters.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserIdentity> _userManager;
        public UserService(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = user.Adapt<UserIdentity>();
            return await _userManager.CheckPasswordAsync(result, password);
        }

        public async Task<Result> CreateAsync(User user)
        {
            var userIdentity = user.Adapt<UserIdentity>();
            var response = await _userManager.CreateAsync(userIdentity, user.PasswordHash);
            if (response.Succeeded) await _userManager.AddToRoleAsync(userIdentity, user.Role);

            return response.Succeeded ? Result.Success() : Result.Failure(response.Errors.Select(e => e.Description));
        }        

        public async Task<Result<User>> FindByUserNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);

            if (result is null) return Result.Failure<User>("User not found");

            var role = await _userManager.GetRolesAsync(result);

            var user = User.Create(
                result.UserName!,
                result.PasswordHash!,
                result.PatientFullName,
                result.Email!,
                result.PatientId,
                result.Id,
                role.First());

            return Result.Success(user.Value!);
        }    
    }
}
