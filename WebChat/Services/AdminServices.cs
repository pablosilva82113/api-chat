using Microsoft.EntityFrameworkCore;
using WebChat.Models;
using WebChat.Tools;

namespace WebChat.Services
{
    public interface IAdminService
    {
        Task<Response> AddBusines(BusinessADD model);
        Task<Response> ListBusiness();
        Task<Response> GetBusiness(int id);
        Task<Response> UpdateBusines(int id, BusinessADD model);
        Task<Response> CreateRole(RoleAdd model);
        Task<Response> GetRoles();
        Task<Response> UpdateRole(int id, RoleAdd model);
        Task<Response> CreateArea(AreaBusinessAdd model);
        Task<Response> GetAreas(int businessId);
        Task<Response> UpdateArea(int id, AreaBusinessAdd model);
    }

    public class AdminServices : IAdminService
    {
        private readonly ChatDbContext _context;
        private readonly Response _dataResponse;

        public AdminServices(ChatDbContext context, Response dataResponse)
        {
            _context = context;
            _dataResponse = dataResponse;
        }

        public async Task<Response> AddBusines(BusinessADD model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var business = new Business
                    {
                        Name = model.Name,
                        Mail = model.Mail,
                        PhoneNumber = model.PhoneNumber,
                        JsonConfig = model.JsonConfig,
                        Key = model.Key,
                        BusinessTimeZone = model.BusinessTimeZone
                    };

                    _context.Businesses.Add(business);
                    await _context.SaveChangesAsync();

                    _dataResponse.SetCode(201);
                    _dataResponse.SetMinimus("Empresa creada correctamente", true);
                    _dataResponse.SetData(business);
                }
                else
                {
                    var businessFind = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == model.Id);
                    if (businessFind == null)
                    {
                        _dataResponse.SetCode(404);
                        _dataResponse.SetMinimus("No existe la empresa", false);
                        return _dataResponse;
                    }

                    businessFind.Name = model.Name;
                    businessFind.PhoneNumber = model.PhoneNumber;
                    businessFind.Mail = model.Mail;
                    businessFind.JsonConfig = model.JsonConfig;
                    businessFind.BusinessTimeZone = model.BusinessTimeZone;

                    _context.Entry(businessFind).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    _dataResponse.SetCode(200);
                    _dataResponse.SetMinimus("Empresa actualizada correctamente", true);
                    _dataResponse.SetData(businessFind);
                }
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> ListBusiness()
        {
            try
            {
                var list = await _context.Businesses.ToListAsync();

                if (list.Count == 0)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("No hay empresas registradas", false);
                    return _dataResponse;
                }

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Lista de empresas obtenida", true);
                _dataResponse.SetDataList(list);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> GetBusiness(int id)
        {
            try
            {
                var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);

                if (business == null)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("La empresa no existe", false);
                    return _dataResponse;
                }

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Empresa encontrada", true);
                _dataResponse.SetData(business);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> UpdateBusines(int id, BusinessADD model)
        {
            try
            {
                var searchBusiness = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);

                if (searchBusiness == null)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("No existe la empresa", false);
                    return _dataResponse;
                }

                searchBusiness.Name = model.Name;
                searchBusiness.PhoneNumber = model.PhoneNumber;
                searchBusiness.JsonConfig = model.JsonConfig;
                searchBusiness.Key = model.Key;
                searchBusiness.BusinessTimeZone = model.BusinessTimeZone;

                _context.Entry(searchBusiness).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Empresa actualizada correctamente", true);
                _dataResponse.SetData(searchBusiness);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> CreateRole(RoleAdd model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    _dataResponse.SetCode(400);
                    _dataResponse.SetMinimus("El nombre del rol es obligatorio", false);
                    return _dataResponse;
                }

                var role = new Role { Name = model.Name };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                _dataResponse.SetCode(201);
                _dataResponse.SetMinimus("Rol creado correctamente", true);
                _dataResponse.SetData(role);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> GetRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                if (roles.Count == 0)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("No hay roles disponibles", false);
                    return _dataResponse;
                }

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Lista de roles obtenida", true);
                _dataResponse.SetDataList(roles);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> UpdateRole(int id, RoleAdd model)
        {
            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("Rol no encontrado", false);
                    return _dataResponse;
                }

                role.Name = model.Name;
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Rol actualizado correctamente", true);
                _dataResponse.SetData(role);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> CreateArea(AreaBusinessAdd model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var area = new AreaBusiness
                    {
                        Name = model.Name,
                        Description = model.Description,
                        BusinessId = model.BusinessId
                    };

                    _context.AreaBusinesses.Add(area);
                    await _context.SaveChangesAsync();

                    _dataResponse.SetCode(201);
                    _dataResponse.SetMinimus("Área creada correctamente", true);
                    _dataResponse.SetData(area);
                }
                else
                {
                    var areaFind = await _context.AreaBusinesses.FirstOrDefaultAsync(a => a.Id == model.Id);

                    if (areaFind == null)
                    {
                        _dataResponse.SetCode(404);
                        _dataResponse.SetMinimus("Área no encontrada", false);
                        return _dataResponse;
                    }

                    areaFind.Name = model.Name;
                    areaFind.Description = model.Description;
                    areaFind.BusinessId = model.BusinessId;

                    _context.Entry(areaFind).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    _dataResponse.SetCode(200);
                    _dataResponse.SetMinimus("Área actualizada correctamente", true);
                    _dataResponse.SetData(areaFind);
                }
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> GetAreas(int businessId)
        {
            try
            {
                var areas = await _context.AreaBusinesses
                    .Where(a => a.BusinessId == businessId)
                    .ToListAsync();

                if (areas.Count == 0)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("No hay áreas para esta empresa", false);
                    return _dataResponse;
                }

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Áreas encontradas", true);
                _dataResponse.SetDataList(areas);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }

        public async Task<Response> UpdateArea(int id, AreaBusinessAdd model)
        {
            try
            {
                var area = await _context.AreaBusinesses.FirstOrDefaultAsync(a => a.Id == id);

                if (area == null)
                {
                    _dataResponse.SetCode(404);
                    _dataResponse.SetMinimus("Área no encontrada", false);
                    return _dataResponse;
                }

                area.Name = model.Name;
                area.Description = model.Description;

                _context.Entry(area).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _dataResponse.SetCode(200);
                _dataResponse.SetMinimus("Área actualizada correctamente", true);
                _dataResponse.SetData(area);
            }
            catch (Exception ex)
            {
                _dataResponse.SetCode(500);
                _dataResponse.SetMinimus($"Error interno: {ex.Message}", false);
            }

            return _dataResponse;
        }
    }
}
