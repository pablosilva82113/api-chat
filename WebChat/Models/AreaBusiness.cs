using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class AreaBusiness
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int BusinessId { get; set; }

    public virtual Business Business { get; set; } = null!;
    public ICollection<Employee>? Employees { get; set; }
}
