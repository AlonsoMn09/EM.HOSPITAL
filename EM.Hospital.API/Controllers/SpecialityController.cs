using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public SpecialityController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSpecialityCommand command)
        {
            var result = await _dispatcher.SendAsync<CreateSpecialityCommand, Result<Guid>>(command);
            return HandleResult(result);
        }
    }
}
