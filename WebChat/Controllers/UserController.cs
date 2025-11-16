using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChat.Models;
using WebChat.Services;
using WebChat.Tools;
using static WebChat.Services.IUserService;

namespace WebChat.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly ChatDbContext Context;
        private Response dataResponse;
        private readonly IUserService userService;

        protected IActionResult DataResponse() => Ok(dataResponse);

        public UserController(ChatDbContext Context,Response dataResponse,IUserService userService)
        {
            this.Context = Context;
            this.dataResponse = dataResponse;
            this.userService = userService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser(EmployeeAdd model)
        {
            try
            {
                var response = await userService.AddUser(model);

                // Aquí decides según el código qué devolver
                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
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
        [Authorize]
        [HttpGet("{businessId:int}")]
        public async Task<IActionResult> GetUsersByBusiness(int businessId)
        {

            try
            {
                var response = await userService.GetUsersByBusiness(businessId);
                if (response.code >= 400)
                {
                    return StatusCode(response.code, response);
                }
                return Ok(response);

            }
            catch (Exception ex)
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

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetUserById(int id)
        {
            try
            {
                var response = await userService.GetEmployeeById(id);
                if(response.code >= 400)
                {
                    return StatusCode(response.code, response);
                }
                return Ok(response);
            }
            catch(Exception ex)
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
