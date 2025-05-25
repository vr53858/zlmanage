using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Mappers;

public static class LetMapper
{
    public static Let ToEntity(this LetCreateRequest r)
        => new Let {
            Datum_polaska = DateOnly.FromDateTime(r.DatumPolaska.Value),
            Datum_dolaska = DateOnly.FromDateTime(r.DatumDolaska.Value),
            Id_zrakoplova = r.IdZrakoplova,
            Id_piste = r.IdPiste,
            Kreirao_ga = r.KreiraoGa
        };

    public static void Map(this LetUpdateRequest r, Let entity)
    {
        entity.Datum_polaska = DateOnly.FromDateTime(r.DatumPolaska.Value);
        entity.Datum_dolaska = DateOnly.FromDateTime(r.DatumDolaska.Value);
        entity.Id_zrakoplova = r.IdZrakoplova;
        entity.Id_piste = r.IdPiste;
        entity.Kreirao_ga = r.KreiraoGa;
    }

    public static LetGetResponse ToResponse(this Let e)
        => new LetGetResponse {
            BrojLeta = e.Broj_leta,
            DatumPolaska = e.Datum_polaska.Value.ToDateTime(TimeOnly.MinValue),
            DatumDolaska = e.Datum_dolaska.Value.ToDateTime(TimeOnly.MinValue),
            IdZrakoplova = e.Id_zrakoplova,
            IdPiste = e.Id_piste,
            KreiraoGa = e.Kreirao_ga
        };
}
