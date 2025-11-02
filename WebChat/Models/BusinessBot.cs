using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class BusinessBot
{
    public int Id { get; set; }

    public int BusinessId { get; set; }

    public string? StructBotJson { get; set; }

    public string? TypeChatBot { get; set; }

    public virtual Business Business { get; set; } = null!;
}
