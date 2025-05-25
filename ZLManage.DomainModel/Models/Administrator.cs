using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Administrator
{
    public int Admin_id { get; set; }

    public int? Razina_prava { get; set; }

    public virtual ICollection<Zaposlenik> Zaposlenik { get; set; } = new List<Zaposlenik>();
}
