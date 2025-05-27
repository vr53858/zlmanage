using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.ApplicationServices.Services.Zaposlenik;

public interface IZaposlenikService
{
    Task<IEnumerable<ZaposlenikGetResponse>> GetAllAsync();
    Task<ZaposlenikGetResponse> GetByIdAsync(int id);
    Task<ZaposlenikGetResponse> CreateAsync(ZaposlenikCreateRequest request);
    Task<bool> UpdateAsync(ZaposlenikUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}