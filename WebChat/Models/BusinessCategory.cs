using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class BusinessCategory
{
    public int CategoryId { get; set; }

    public int BusinessId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Business Business { get; set; } = null!;
}
