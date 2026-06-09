using Microsoft.AspNetCore.Mvc;
using MiRenta.Application.Common.Models;
using System.Security.Claims;

namespace MiRenta.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected Guid UserId =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        protected IActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(result.Data);

            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { error = result.Error }),
                ErrorType.Conflict => Conflict(new { error = result.Error }),
                _ => BadRequest(new { error = result.Error })
            };
        }

        protected IActionResult ToActionResult(Result result)
        {
            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { error = result.Error }),
                ErrorType.Conflict => Conflict(new { error = result.Error }),
                _ => BadRequest(new { error = result.Error })
            };
        }
    }
}
