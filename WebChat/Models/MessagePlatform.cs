using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class MessagePlatform
{
    public int Id { get; set; }

    public string NamePlatform { get; set; } = null!;

    public virtual ICollection<MessageContactEmployee> MessageContactEmployees { get; set; } = new List<MessageContactEmployee>();
}
