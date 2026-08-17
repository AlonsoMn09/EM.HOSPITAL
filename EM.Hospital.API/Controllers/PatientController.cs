using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Patient.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public PatientController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePatientCommand command)
        {
            var result = await _dispatcher.SendAsync<CreatePatientCommand, Result<Guid>>(command);
            return HandleResult(result);
        }
    }
}
