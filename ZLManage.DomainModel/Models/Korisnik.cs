using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Korisnik
{
    public int Id_korisnika { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public string? Email { get; set; }

    public virtual Klijent? Klijent { get; set; }

    public virtual Zaposlenik? Zaposlenik { get; set; }
}
