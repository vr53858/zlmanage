using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Zaposlenik
{
    public int Id_zaposlenika { get; set; }

    public string? Pozicija { get; set; }

    public int? Kreirao_ga_admin { get; set; }

    public virtual Korisnik Id_zaposlenikaNavigation { get; set; } = null!;

    public virtual Administrator? Kreirao_ga_adminNavigation { get; set; }

    public virtual ICollection<Let> Let { get; set; } = new List<Let>();
}
