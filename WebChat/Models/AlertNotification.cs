using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class AlertNotification
{
    public int Id { get; set; }

    public int BusinessId { get; set; }

    public string? DataJson { get; set; }

    public DateTime? DateInsert { get; set; }

    public virtual Business Business { get; set; } = null!;
}
