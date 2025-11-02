using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class MessageContactEmployee
{
    public int Id { get; set; }

    public int MessagePlatformId { get; set; }

    public int EmployeeId { get; set; }

    public int ContactId { get; set; }

    public int BusinessId { get; set; }

    public string? ChatJson { get; set; }

    public DateTime? DateInit { get; set; }

    public DateTime? DateClose { get; set; }

    public string? Status { get; set; }

    public bool? See { get; set; }

    public DateTime? DateReasign { get; set; }

    public int? EmployeeIdReasign { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual Contact Contact { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual MessagePlatform MessagePlatform { get; set; } = null!;
}
