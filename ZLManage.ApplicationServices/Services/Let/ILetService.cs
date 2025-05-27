using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Services.Let;

public interface ILetService
{
    Task<IEnumerable<LetGetResponse>> GetAllAsync();
    Task<LetGetResponse> GetByIdAsync(int id);
    Task<LetGetResponse> CreateAsync(LetCreateRequest req);
    Task<bool> UpdateAsync(LetUpdateRequest req);
    Task<bool> DeleteAsync(int id);
}