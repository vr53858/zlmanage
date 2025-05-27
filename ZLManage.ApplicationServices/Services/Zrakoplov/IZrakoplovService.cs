using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Services.Zrakoplov;

public interface IZrakoplovService
{
    Task<IEnumerable<ZrakoplovGetResponse>> GetAllAsync();
    Task<ZrakoplovGetResponse> GetByIdAsync(int id);
    Task<ZrakoplovGetResponse> CreateAsync(ZrakoplovCreateRequest request);
    Task<bool> UpdateAsync(ZrakoplovUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}