using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Mappers;

public static class ZaposlenikMapper
{
    public static Zaposlenik ToEntity(this ZaposlenikCreateRequest r)
        => new Zaposlenik {
            Pozicija = r.Pozicija,
            Kreirao_ga_admin = r.KreiraoGaAdmin
        };

    public static void Map(this ZaposlenikUpdateRequest r, Zaposlenik e)
    {
        e.Pozicija = r.Pozicija;
        e.Kreirao_ga_admin = r.KreiraoGaAdmin;
    }

    public static ZaposlenikGetResponse ToResponse(this Zaposlenik e)
        => new ZaposlenikGetResponse {
            IdZaposlenika = e.Id_zaposlenika,
            Pozicija = e.Pozicija,
            KreiraoGaAdmin = e.Kreirao_ga_admin
        };
}