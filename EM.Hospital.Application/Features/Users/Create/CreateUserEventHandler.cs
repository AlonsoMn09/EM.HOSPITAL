using EM.Hospital.Application.Common.Contracts.Services.Auth;
using EM.Hospital.Application.Common.Models.Auth;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Events;
using EM.Hospital.Domain.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Users.Create
{
    public class CreateUserEventHandler : IDomainEventHandler<CreatePatientDomainEvent>
    {
        private readonly IUserService _service;
        public CreateUserEventHandler(IUserService service)
        {
            _service = service;
        }
        public async Task<Result> HandlerAsync(CreatePatientDomainEvent domainEvent)
        {
            var user = User.Create(domainEvent.UserName, domainEvent.PasswordHash, domainEvent.FullName, domainEvent.Email, domainEvent.PatientId, domainEvent.Id.ToString(), domainEvent.Role);
            if(!user.IsSuccess) return Result.Failure(user.Message!);
            var result = await _service.CreateAsync(user.Value!);
            if(result.IsFailure)return Result.Failure(result.Errors!);
            return Result.Success();
        }
    }
}
