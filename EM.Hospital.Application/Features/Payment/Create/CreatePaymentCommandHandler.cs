using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Payment.Create
{
    public class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand, Result<Guid>>
    {
        private readonly IPaymentRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentCommandHandler(IPaymentRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> HandlerAsync(CreatePaymentCommand command, CancellationToken cancellationToken = default)
        {
            var amount = EM.Hospital.Domain.ValueObjects.Money.Create(command.Amount, command.Currency);

            var payment = EM.Hospital.Domain.Entities.Payment.Create(command.AppointmentId, amount.Value, command.Date, Domain.Enums.PaymentMethod.Cash, Domain.Enums.PaymentStatus.Completed);
            await _repo.AddAsync(payment.Value!);
            await _unitOfWork.SaveChangeAsync();
            return Result<Guid>.Success(payment.Value!.Id);
        }
    }
}
