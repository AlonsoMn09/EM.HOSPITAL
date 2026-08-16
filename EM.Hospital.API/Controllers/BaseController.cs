using EM.Hospital.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{

    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess) return Ok(result);

            if (result.Errors!.Any())
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Error = result.Errors,
                    timespan = DateTime.UtcNow
                });
            }
            return BadRequest(result);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess) return Ok(result);

            if (result.Errors!.Any())
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Error = result.Errors,
                    timespan = DateTime.UtcNow
                });
            }
            return BadRequest(result);
        }
    }
}
