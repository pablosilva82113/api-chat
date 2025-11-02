using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public ICollection<Employee>? Employees { get; set; }
}
