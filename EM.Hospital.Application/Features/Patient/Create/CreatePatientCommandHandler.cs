using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.Create
{
    public class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Result<Guid>>
    {
        private readonly IPatientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> HandlerAsync(CreatePatientCommand command, CancellationToken cancellationToken = default)
        {
            var client = await _repository.FindByPredicateAsync(p => p.Document.Type == command.DocumentType && p.Document.Document == command.DocumentNumber);
            if (client == null)
                return Result.Failure<Guid>("Patient with the same document already exists");
            var document = IdentityDocument.Create(command.DocumentType, command.DocumentNumber); 
            if(!document.IsSuccess)
                return Result.Failure<Guid>(document.Message!);

            var patient = EM.Hospital.Domain.Entities.Patient.Create(command.FirstName, command.LastName, command.Email, command.Phone, document.Value, command.UserName, command.Password);
            if (!patient.IsSuccess)
                return Result.Failure<Guid>(patient.Message!);

            await _repository.AddAsync(patient.Value!);
            var saveResult = await _unitOfWork.SaveChangeAsync(cancellationToken);
            if (saveResult.IsFailure)
                return Result.Failure<Guid>(saveResult.Errors!);
            return Result.Success(patient.Value!.Id);
        }
    }
}
