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
        Task<Response>UpdateUser(int employeId,EmployeeAdd model);
        Task<Response> CreateContact(ContactAdd model);
        Task<Response>GetContacsByBusiness(int businessId);
        Task<Response>GetContactById(int id);
        Task<Response> UpdateContact(int contactId, ContactAdd model);

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

            public async Task<Response> UpdateUser(int employeId, EmployeeAdd model)
            {
                try
                {
                    var user = await Context.Employees.FirstOrDefaultAsync(e => e.Id == model.Id);
                    if(user == null)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("Empleado no encontardo");
                        return dataResponse;
                    }
                    user.Name=model.Name;
                    user.AreaId = model.AreaId;
                    user.RoleId = model.RoleId;
                    user.Mail = model.Mail;

                    Context.Entry(user).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Empresa actualizada correctamente", true);
                    dataResponse.SetData(user);

                }
                catch(Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }

            public async Task<Response> CreateContact(ContactAdd model)
            {
                try
                {
                    if (model == null)
                    {
                        dataResponse.SetCode(400);
                        dataResponse.SetMinimus("No puede estar vacio el modelo");
                        return dataResponse;
                    }
                    var newContac = new Contact()
                    {
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        Mail = model.Mail,
                        BusinesId = model.BusinesId
                    };
                     Context.Contacts.Add(newContac);
                    await Context.SaveChangesAsync();
                    dataResponse.SetCode(201);
                    dataResponse.SetMinimus("Contacto agregado con exito",true);
                    dataResponse.SetData(newContac);
                }catch(Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }

            public async Task<Response> GetContacsByBusiness(int businessId)
            {
                try
                {
                    

                    var getContacts = await Context.Contacts.Where(c => c.BusinessId == businessId).ToListAsync();

                    if (getContacts.Count == 0)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("Business sin contactos");
                        return dataResponse;

                    }

                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Lista de contactos", true);
                    dataResponse.SetDataList(getContacts);
                }
                catch (Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }

            public async Task<Response> GetContactById(int id)
            {
                try
                {
                    var contact = await Context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

                    if (contact == null) 
                    {
                        dataResponse.SetCode(401);
                        dataResponse.SetMinimus("Contacto no encontardo");
                    }
                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Contacto",true);
                    dataResponse.SetData(contact);

                }catch(Exception ex)
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {{ex.Message}}", false);
                }
                return dataResponse;
            }

            public async Task<Response> UpdateContact(int contactId, ContactAdd model)
            {
                try
                {
                    var contact = await Context.Contacts.FirstOrDefaultAsync(c=> c.Id==contactId);

                    if(contact == null)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("Contacto no encontrado",false);
                        return dataResponse;
                    }
                    contact.Name = model.Name;
                    contact.PhoneNumber = model.PhoneNumber;
                    contact.Mail = model.Mail;

                    Context.Entry(contact).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Contacto actualizado correctamente", true);

                }
                catch(Exception ex)
                {
                   dataResponse.SetCode(500);
                   dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }
        }
    }
}
