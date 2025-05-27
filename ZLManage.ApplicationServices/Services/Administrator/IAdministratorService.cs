using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Services.Administrator;

public interface IAdministratorService
{
    Task<IEnumerable<AdministratorGetResponse>> GetAllAsync();
    Task<AdministratorGetResponse> GetByIdAsync(int id);
    Task<AdministratorGetResponse> CreateAsync(AdministratorCreateRequest req);
    Task<bool> UpdateAsync(AdministratorUpdateRequest req);
    Task<bool> DeleteAsync(int id);
}