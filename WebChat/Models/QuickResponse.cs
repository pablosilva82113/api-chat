using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class QuickResponse
{
    public int Id { get; set; }

    public int BusinessId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Business Business { get; set; } = null!;
}
