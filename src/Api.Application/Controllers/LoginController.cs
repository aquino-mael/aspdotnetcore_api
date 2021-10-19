using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] UserEntity user, [FromServices] ILoginService service)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      else if (user == null) return BadRequest();

      try
      {
        var result = await service.FindByLogin(user);

        if (result == null) return NotFound();

        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
  }
}
