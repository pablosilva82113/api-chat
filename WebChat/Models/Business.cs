using System;
using System.Collections.Generic;

namespace WebChat.Models;

public partial class Business
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Mail { get; set; }

    public string? PhoneNumber { get; set; }

    public string? JsonConfig { get; set; }

    public string? Key { get; set; }

    public string? BusinessTimeZone { get; set; }

    public virtual ICollection<AlertNotification> AlertNotifications { get; set; } = new List<AlertNotification>();

    public virtual ICollection<AreaBusiness> AreaBusinesses { get; set; } = new List<AreaBusiness>();

    public virtual ICollection<BusinessBot> BusinessBots { get; set; } = new List<BusinessBot>();

    public virtual ICollection<BusinessCategory> BusinessCategories { get; set; } = new List<BusinessCategory>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<MessageContactEmployee> MessageContactEmployees { get; set; } = new List<MessageContactEmployee>();

    public virtual ICollection<QuickResponse> QuickResponses { get; set; } = new List<QuickResponse>();
}
