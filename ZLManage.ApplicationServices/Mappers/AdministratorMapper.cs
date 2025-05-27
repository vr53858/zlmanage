using ZLManage.DomainModel.Models;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Mappers;

public static class AdministratorMapper
{
    public static Administrator ToEntity(this AdministratorCreateRequest r)
        => new Administrator {
            Admin_id = r.AdminId,
            Razina_prava = r.RazinaPrava
        };

    public static void Map(this AdministratorUpdateRequest r, Administrator e)
        => e.Razina_prava = r.RazinaPrava;

    public static AdministratorGetResponse ToResponse(this Administrator e)
        => new AdministratorGetResponse {
            AdminId = e.Admin_id,
            RazinaPrava = e.Razina_prava
        };
}