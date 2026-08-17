using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Doctor.Create;
using EM.Hospital.Application.Features.Prescription.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public PrescriptionController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePrescriptionCommand command)
        {
            var result = await _dispatcher.SendAsync<CreatePrescriptionCommand, Result>(command);
            return HandleResult(result);
        }
    }
}
