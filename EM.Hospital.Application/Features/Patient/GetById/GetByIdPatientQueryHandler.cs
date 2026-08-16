using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Features.Patient.GetById.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.GetById
{
    public class GetByIdPatientQueryHandler : IQueryHandler<GetByIdPatientQuery, Result<GetPatientResponse>>
    {
        private readonly IPatientRepository _repository;
        public GetByIdPatientQueryHandler(IPatientRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<GetPatientResponse>> HandleAsync(GetByIdPatientQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _repository.GetByIdAsync(query.Id);
            if (result is null) return Result.Failure<GetPatientResponse>("Patient not found.");
            return Result.Success(new GetPatientResponse
            {
                LastName = result.LastName,
                FirstName = result.FirstName,
                DocumentNumber = result.Document.Document,
                DocumentType = result.Document.Type,
                Email = result.Email,
                Phone = result.Phone
            });
        }
    }
}
