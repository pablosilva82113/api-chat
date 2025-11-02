using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Tools;

namespace WebChat.Services
{
    public interface IUserService
    {
        Task<Response> AddUser(EmployeeAdd model);
        Task<Response>GetEmployeeById(int id);
        Task<Response> GetUsersByBusiness(int businessId);


        public class UserService: IUserService
        {
            public ChatDbContext Context;
            protected Response dataResponse;

            public UserService(ChatDbContext Context, Response dataResponse)
            {
                this.Context = Context;
                this.dataResponse = dataResponse;
            }
            public async Task<Response> AddUser(EmployeeAdd model)
            {
                try
                {
                    if (model.Id == 0)
                    {
                        var employee = new Employee()
                        {
                            Name = model.Name,                  // puede venir null
                            AreaId = model.AreaId,                  // puede venir null
                            BusinessId = model.BusinessId,      // puede venir null
                            RoleId = model.RoleId,                  // puede venir null
                            PasswordHashed = PasswordHelper.HashPassword(model.PasswordHashed), // puede venir null
                            Mail = model.Mail                   // puede venir null
                        };

                        Context.Add(employee);
                        await Context.SaveChangesAsync();

                        dataResponse.SetCode(201);
                        dataResponse.SetMinimus("Usuario creado", true);
                        dataResponse.SetData(employee);
                    }
                    else
                    {
                        dataResponse.SetCode(400);
                        dataResponse.SetMinimus("El ID debe ser 0 para crear un nuevo usuario", false);
                    }
                }
                catch (Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }

                return dataResponse;
            }

            public async Task<Response> GetUsersByBusiness(int businessId)
            {
                try
                {
                   
                    var employeeList = await Context.Employees
                        .Where(e => e.BusinessId == businessId)
                        .Select(e => new EmployeeAdd()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Mail = e.Mail,
                            RoleId = e.RoleId,   // navegación Role
                            AreaId = e.AreaId,    // navegación Area
                            BusinessId = e.BusinessId,
                        })
                        .ToListAsync();
                    if (employeeList.Count<=0)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("No hay agentes en esa empresa");
                        return dataResponse;
                    }
                    dataResponse.SetCode(202);
                    dataResponse.SetMinimus("Lista de usuarios", true);
                    dataResponse.SetDataList(employeeList);
                }
                catch (Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}",false);
                }
                return dataResponse;
            }

            public async Task<Response> GetEmployeeById(int id)
            {
                try
                {
                    var employee = await Context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                    if (employee == null)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("Empleado no encontrado");

                        return dataResponse;
                    }
                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Empleado encontrado", true);
                    dataResponse.SetData(employee);
                }
                catch (Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }


        }
    }
}
