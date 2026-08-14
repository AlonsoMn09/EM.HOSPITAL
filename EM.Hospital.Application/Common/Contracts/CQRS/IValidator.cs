using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.CQRS
{
    public interface IValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellationToken = default!);
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; set; } = new();
    }

    public class ValidationError
    {
        public string PropertyName { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
