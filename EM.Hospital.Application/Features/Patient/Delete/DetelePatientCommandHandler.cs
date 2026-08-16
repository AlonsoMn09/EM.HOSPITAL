using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.Delete
{
    public class DetelePatientCommandHandler : ICommandHandler<DeletePatientCommand, Result>
    {
        private readonly IPatientRepository _repository;
        private readonly IUnitOfWork _unitOfwork;
        public DetelePatientCommandHandler(IPatientRepository repository, IUnitOfWork unitOfwork)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
        }
        public async Task<Result> HandlerAsync(DeletePatientCommand command, CancellationToken cancellationToken = default)
        {
            var patient = await _repository.GetByIdAsync(command.Id);
            if (patient == null) return Result.Failure("Patient not found.");

            patient.SoftDelete();

            var saveResult = await _unitOfwork.SaveChangeAsync();

            if (saveResult.IsFailure)
                return Result.Failure(saveResult.Message!);

            return Result.Success("Patient deleted successfully");
        }
    }
}
