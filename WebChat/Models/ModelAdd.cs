using System.ComponentModel.DataAnnotations;

namespace WebChat.Models

{
    public class ModelAdd
    {
    }

    public class BusinessADD
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string? Mail { get; set; }

        public string? PhoneNumber { get; set; }

        public string? JsonConfig { get; set; }

        public string? Key { get; set; }

        public string? BusinessTimeZone { get; set; }
    }


    public class RoleAdd
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = null!;
    }

    public class AreaBusinessAdd
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "La descriocion es requerida")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "El id de la empresa es obligatorio")]
        public int BusinessId { get; set; }
    }

    public class EmployeeAdd
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "El area es obligatoria")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string? Mail { get; set; }
        [Required(ErrorMessage = "El idbusiness es obligatorio")]
        public int BusinessId { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]
        public string? PasswordHashed { get; set; }
    }

    public class LoginModel
    {
        public string? Mail { get; set; }
        public string? PasswordHashed { get; set; }
    }
}
