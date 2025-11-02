using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Tools;

namespace WebChat.Services
{
    public interface IAuthService
    {
        Task<Response>Login(LoginModel model);
        Task<Response>RestorePassword(LoginModel model);


        public class AuthService : IAuthService
        {
            public ChatDbContext Context;
            protected Response dataResponse;
            public IJwtTool JwtTool;
            public AuthService(ChatDbContext Context, Response dataResponse, IJwtTool JwtTool)
            {
                this.Context = Context;
                this.dataResponse = dataResponse;
                this.JwtTool = JwtTool;

            }
            public async Task<Response> Login(LoginModel model) 
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.Mail))
                    {
                        dataResponse.SetCode(401);
                        dataResponse.SetMinimus("Ingresa el correo",false);
                        return dataResponse;
                    }
                    var searchMail = await Context.Employees.FirstOrDefaultAsync(r => r.Mail == model.Mail);
                    if (searchMail == null)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("Usuario no encontrado", false);
                        return dataResponse;
                       
                    }
                    var validatePassword = PasswordHelper.VerifyPassword(searchMail.PasswordHashed, model.PasswordHashed);
                    if (!validatePassword)
                    {
                        dataResponse.SetCode(400);
                        dataResponse.SetMinimus("Verifica los datos ingresados", false);
                        return dataResponse;
                    }
                    var getRole = await Context.Roles.FirstOrDefaultAsync(r => r.Id == searchMail.RoleId);
                    //generar el token 
                    var token = JwtTool.GenerateToken(
                        searchMail.Id.ToString(),
                        searchMail.Name,
                        getRole.Name
                        );
                    var result = new
                    {
                        Token = token,
                        Employee = new
                        {
                            searchMail.Id,
                            searchMail.Name,
                            searchMail.Mail,
                            Role = searchMail.Role?.Name
                        }
                    };
                    dataResponse.SetCode(202);
                    dataResponse.SetMinimus("Bienvenido");
                    dataResponse.SetData(result);
                }
                catch (Exception ex) 
                {
                    dataResponse.SetCode(500);
                    dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
                }
                return dataResponse;
            }

            public async Task<Response> RestorePassword(LoginModel model)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.Mail))
                    {
                        dataResponse.SetCode(400);
                        dataResponse.SetMinimus("Ingresa un correo válido", false);
                        return dataResponse;
                    }

                    var validateEmail = await Context.Employees.FirstOrDefaultAsync(e=>e.Mail == model.Mail);
                    if (validateEmail == null)
                    {
                        dataResponse.SetCode(404);
                        dataResponse.SetMinimus("No existe ningún usuario con ese correo", false);
                        return dataResponse;
                    }
                    var newPassword = PasswordHelper.HashPassword(model.PasswordHashed);

                    validateEmail.PasswordHashed = newPassword;
                    Context.Employees.Update(validateEmail);
                    await Context.SaveChangesAsync();
                    dataResponse.SetCode(200);
                    dataResponse.SetMinimus("Contraseña restablecida correctamente");
                    dataResponse.SetData(validateEmail);
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
