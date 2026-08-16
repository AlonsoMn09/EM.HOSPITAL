using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.DTO;
using EM.Hospital.Application.Common.Utils;
using EM.Hospital.Application.Features.Patient.List.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.List
{
    public class ListPatientQueryHandler : IQueryHandler<ListPatientQuery, Result<PagedResponse<ListPatientResponse>>>
    {
        private readonly IPatientRepository _repository;
        public ListPatientQueryHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResponse<ListPatientResponse>>> HandleAsync(ListPatientQuery query, CancellationToken cancellationToken = default)
        {
            var response = new PagedResponse<ListPatientResponse>();

            var result = await _repository.ListAsync
                (
                    predicate: p => p.Active,
                    selector: p => new ListPatientResponse
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        Phone = p.Phone,
                        DocumentType = p.Document.Type,
                        DocumentNumber = p.Document.Document
                    },
                    orderBy: p => p.FirstName,
                    page: query.PageNumber,
                    pageSize: query.PageSize
                );

            response.Data = result.Result;
            response.TotalRows = result.TotalCount;
            response.TotalRowsPerPage = response.Data.Count;
            response.TotalPages = Helpers.CalculateTotalPages(result.TotalCount, query.PageSize).Value;

            return Result.Success(response);
        }
    }
}
