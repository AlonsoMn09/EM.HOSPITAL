using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Appointment.Create;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AppointmentController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentCommand command)
        {
            var result = await _dispatcher.SendAsync<CreateAppointmentCommand, Result<Guid>>(command);
            return HandleResult(result);
        }
    }
}
