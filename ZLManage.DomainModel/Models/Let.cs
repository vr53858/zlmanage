using System;
using System.Collections.Generic;

namespace ZLManage.DomainModel.Models;

public partial class Let
{
    public int Broj_leta { get; set; }

    public DateOnly? Datum_polaska { get; set; }

    public DateOnly? Datum_dolaska { get; set; }

    public int? Id_zrakoplova { get; set; }

    public int? Id_piste { get; set; }

    public int? Kreirao_ga { get; set; }

    public virtual Pista? Id_pisteNavigation { get; set; }

    public virtual Zrakoplov? Id_zrakoplovaNavigation { get; set; }

    public virtual Zaposlenik? Kreirao_gaNavigation { get; set; }
}
