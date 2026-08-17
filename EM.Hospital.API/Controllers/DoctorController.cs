using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Doctor.Create;
using EM.Hospital.Application.Features.Patient.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public DoctorController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDoctorCommand command)
        {
            var result = await _dispatcher.SendAsync<CreateDoctorCommand, Result<Guid>>(command);
            return HandleResult(result);
        }
    }
}
