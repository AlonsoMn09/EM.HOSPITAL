using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Auth.Login;
using EM.Hospital.Application.Features.Auth.Login.DTO;
using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AuthController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginQuery query)
        {
            var result = await _dispatcher.QueryAsync<LoginQuery, Result<LoginResponse>>(query);
            return HandleResult(result);
        }
    }
}
