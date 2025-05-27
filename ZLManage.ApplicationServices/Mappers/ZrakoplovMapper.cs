using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Mappers;

public static class ZrakoplovMapper
{
    public static Zrakoplov ToEntity(this ZrakoplovCreateRequest r)
        => new Zrakoplov {
            Model = r.Model,
            Registracija = r.Registracija
        };

    public static void Map(this ZrakoplovUpdateRequest r, Zrakoplov e)
    {
        e.Model = r.Model;
        e.Registracija = r.Registracija;
    }

    public static ZrakoplovGetResponse ToResponse(this Zrakoplov e)
        => new ZrakoplovGetResponse {
            IdZrakoplova = e.Id_zrakoplova,
            Model = e.Model,
            Registracija = e.Registracija
        };
}