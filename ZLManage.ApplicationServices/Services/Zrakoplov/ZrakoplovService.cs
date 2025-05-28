using FluentValidation;
using ZLManage.ApplicationServices.Mappers;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainModel.Validation.Validators;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.ApplicationServices.Services.Zrakoplov;

public class ZrakoplovService : IZrakoplovService
{
    private readonly IZrakoplovRepository _repo;

    public ZrakoplovService(IZrakoplovRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ZrakoplovGetResponse>> GetAllAsync()
    {
        var list = await _repo.GetZrakoploviAsync();
        return list.Select(z => z.ToResponse());
    }

    public async Task<ZrakoplovGetResponse> GetByIdAsync(int id)
    {
        var entity = await _repo.GetZrakoplovByIdAsync(id);
        return entity?.ToResponse();
    }

    public async Task<ZrakoplovGetResponse> CreateAsync(ZrakoplovCreateRequest request)
    {
        var entity = request.ToEntity();
        await _repo.CreateZrakoplovAsync(entity);
        return entity.ToResponse();
    }

    public async Task<bool> UpdateAsync(ZrakoplovUpdateRequest request)
    {
        var entity = await _repo.GetZrakoplovByIdAsync(request.IdZrakoplova);
        if (entity == null)
            return false;

        request.Map(entity);
        var result = await _repo.UpdateZrakoplovAsync(entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repo.DeleteZrakoplovAsync(id);
        return result > 0;
    }
}