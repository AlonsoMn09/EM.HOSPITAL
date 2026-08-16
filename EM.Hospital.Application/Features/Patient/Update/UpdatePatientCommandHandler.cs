using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.Update
{
    public class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, Result>
    {
        private readonly IPatientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandlerAsync(UpdatePatientCommand command, CancellationToken cancellationToken = default)
        {
            //1. Verificar la existencia del paciente
            var patient = await _repository.GetByIdAsync(command.Id);
            if (patient is null) return Result.Failure("Patient not found");


            //2. crear las instancias con los nuevos valores
            var document = IdentityDocument.Create(command.DocumentType, command.DocumentNumber);

            if (!document.IsSuccess) return Result.Failure(document.Message!);

            var patientUpdated = EM.Hospital.Domain.Entities.Patient.Update(
                patient,
                command.FirstName,
                command.LastName,
                command.Email,
                command.Phone,
                document.Value!);

            if (!patientUpdated.IsSuccess) return Result.Failure(patientUpdated.Message!);

            //3. Guardar los cambios en la base de datos - Unitofwork
            var saveResult = await _unitOfWork.SaveChangeAsync(cancellationToken);

            if (saveResult.IsFailure)
                return Result.Failure(saveResult.Message!);

            return Result.Success("Patient updated successfully");
        }
    }
}
