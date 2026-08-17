using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Doctor.Create;
using EM.Hospital.Application.Features.DoctorSchedule.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        public DoctorScheduleController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDoctorScheduleCommand command)
        {
            var result = await _dispatcher.SendAsync<CreateDoctorScheduleCommand, Result<Guid>>(command);
            return HandleResult(result);
        }
    }
}
