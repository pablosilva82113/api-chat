using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RoleId { get; set; }
    public int AreaId { get; set; }

    public string? Mail { get; set; }

    public int BusinessId { get; set; }

    public string? PasswordHashed { get; set; }

    public virtual Business Business { get; set; } = null!;

    // Relaciones de navegación
    public virtual Role Role { get; set; } = null!;
    public virtual AreaBusiness Area { get; set; } = null!;
    public virtual ICollection<MessageContactEmployee> MessageContactEmployees { get; set; } = new List<MessageContactEmployee>();
}
