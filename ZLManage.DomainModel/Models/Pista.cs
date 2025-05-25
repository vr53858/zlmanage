using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Pista
{
    public int Id_piste { get; set; }

    public int? Duljina { get; set; }

    public string? Oznaka { get; set; }

    public virtual ICollection<Let> Let { get; set; } = new List<Let>();
}
