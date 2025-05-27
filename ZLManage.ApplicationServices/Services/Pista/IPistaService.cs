using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Services.Pista;

public interface IPistaService
{
    Task<IEnumerable<PistaGetResponse>> GetAllAsync();
    Task<PistaGetResponse> GetByIdAsync(int id);
    Task<PistaGetResponse> CreateAsync(PistaCreateRequest req);
    Task<bool> UpdateAsync(PistaUpdateRequest req);
    Task<bool> DeleteAsync(int id);
}