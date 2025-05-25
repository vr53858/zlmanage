using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Klijent
{
    public int Id_korisnika { get; set; }

    public string? Tip_klijenta { get; set; }

    public virtual Korisnik Id_korisnikaNavigation { get; set; } = null!;
}
