using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Mail { get; set; }

    public int? BusinesId { get; set; }

    public int? BusinessId { get; set; }

    public virtual Business? Business { get; set; }

    public virtual ICollection<MessageContactEmployee> MessageContactEmployees { get; set; } = new List<MessageContactEmployee>();
}
