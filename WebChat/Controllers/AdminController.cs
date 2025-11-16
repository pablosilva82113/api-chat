using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChat.Models;
using WebChat.Services;
using WebChat.Tools;
using Microsoft.AspNetCore.Authorization;

namespace WebChat.Controllers
{
    [Route("api/admin/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ChatDbContext _Context;
        private Response dataResponse;
        private readonly IAdminService adminservice;

        protected IActionResult DataResponse() => Ok(dataResponse);

        public AdminController(ChatDbContext Context, Response dataResponse, IAdminService adminservice)
        {
            _Context = Context;
            this.dataResponse = dataResponse;
            this.adminservice = adminservice;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBusines(BusinessADD model)
        {
            try
            {
                var response = await adminservice.AddBusines(model);

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
        [HttpGet]
        public async Task<IActionResult> ListBussines()
        {
            try
            {
                var response = await adminservice.ListBusiness();

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al listar las empresas",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBusine(int id)
        {
            try
            {
                var response = await adminservice.GetBusiness(id);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener la empresa",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBusiens(int id, BusinessADD model)
        {
            try
            {
                var response = await adminservice.UpdateBusines(id, model);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar la empresa",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var response = await adminservice.GetRoles();

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener los roles",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, RoleAdd model)
        {
            try
            {
                var response = await adminservice.UpdateRole(id, model);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el rol",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleAdd model)
        {
            try
            {
                var response = await adminservice.CreateRole(model);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al crear el rol",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateArea(AreaBusinessAdd model)
        {
            try
            {
                var response = await adminservice.CreateArea(model);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al crear el área",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("{businessId:int}")]
        public async Task<IActionResult> GetAreas(int businessId)
        {
            try
            {
                var response = await adminservice.GetAreas(businessId);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener las áreas",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateArea(int id, AreaBusinessAdd model)
        {
            try
            {
                var response = await adminservice.UpdateArea(id, model);

                if (response.code >= 400)
                    return StatusCode(response.code, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el área",
                    code = 500,
                    isOk = false,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    error = ex.Message
                });
            }
        }
    }
}
