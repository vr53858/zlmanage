using ZLManage.ApplicationServices.Mappers;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.ApplicationServices.Services.Zaposlenik;

public class ZaposlenikService : IZaposlenikService
{
    private readonly IZaposlenikRepository _repo;

    public ZaposlenikService(IZaposlenikRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ZaposlenikGetResponse>> GetAllAsync()
    {
        var list = await _repo.GetZaposleniciAsync();
        return list.Select(z => z.ToResponse());
    }

    public async Task<ZaposlenikGetResponse> GetByIdAsync(int id)
    {
        var entity = await _repo.GetZaposlenikByIdAsync(id);
        return entity?.ToResponse();
    }

    public async Task<ZaposlenikGetResponse> CreateAsync(ZaposlenikCreateRequest request)
    {
        var entity = request.ToEntity();
        await _repo.CreateZaposlenikAsync(entity);
        return entity.ToResponse();
    }

    public async Task<bool> UpdateAsync(ZaposlenikUpdateRequest request)
    {
        var entity = await _repo.GetZaposlenikByIdAsync(request.IdZaposlenika);
        if (entity == null)
            return false;

        request.Map(entity);
        var result = await _repo.UpdateZaposlenikAsync(entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repo.DeleteZaposlenikAsync(id);
        return result > 0;
    }
}