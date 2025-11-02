using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChat.Models;
using WebChat.Services;
using WebChat.Tools;

namespace WebChat.Controllers
{
    [Route("api/auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly ChatDbContext Context;
        private Response dataResponse;
        private readonly IAuthService authService;
        
        public AuthController(ChatDbContext Context, Response dataResponse, IAuthService authService)
        {
            this.Context = Context;
            this.dataResponse = dataResponse;
            this.authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var response = await authService.Login(model);
                if (response.code >= 400)
                {
                    return StatusCode(response.code,response);
                }
                return Ok(response);

            }catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult>RestorePassord(LoginModel model)
        {
            try
            {
                var response = await authService.RestorePassword(model);
                if (response.code >= 400)
                {
                    return StatusCode(response.code, response);
                }
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error interno del servidor",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }


    }
}
