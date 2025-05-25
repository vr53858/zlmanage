using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Zrakoplov
{
    public int Id_zrakoplova { get; set; }

    public string? Model { get; set; }

    public string? Registracija { get; set; }

    public virtual ICollection<Let> Let { get; set; } = new List<Let>();
}
