using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Mappers;

public static class PistaMapper
{
    public static Pista ToEntity(this PistaCreateRequest r)
        => new Pista {
            Duljina = r.Duljina,
            Oznaka = r.Oznaka
        };

    public static void Map(this PistaUpdateRequest r, Pista e)
    {
        e.Duljina = r.Duljina;
        e.Oznaka = r.Oznaka;
    }

    public static PistaGetResponse ToResponse(this Pista e)
        => new PistaGetResponse {
            IdPiste = e.Id_piste,
            Duljina = e.Duljina,
            Oznaka = e.Oznaka
        };
}