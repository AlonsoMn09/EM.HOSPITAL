using EM.Hospital.Application.Common.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.Create
{
    public class CreatePatientCommandValidator : IValidator<CreatePatientCommand>
    {
        public Task<ValidationResult> ValidateAsync(CreatePatientCommand instance, CancellationToken cancellationToken = default)
        {
            var result = new ValidationResult { IsValid = true };
            if (instance.DocumentType.Length != 3)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError { PropertyName = nameof(instance.DocumentType), Message = "DocumentType must be exactly 3 characters long." });  
            }
            if (instance.DocumentNumber.Length != 9)
            {
                result.IsValid = false;
                result.Errors.Add(new ValidationError { PropertyName = nameof(instance.DocumentNumber), Message = "DocumentNumber must be exactly 9 characters long." });
            }
            return Task.FromResult(result);
        }
    }
}
