using ZLManage.ApplicationServices.Mappers;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.ApplicationServices.Services.Let;

public class LetService : ILetService
{
    private readonly ILetRepository _repo;
    public LetService(ILetRepository repo) => _repo = repo;

    public async Task<IEnumerable<LetGetResponse>> GetAllAsync() =>
        (await _repo.GetLetoviAsync())
        .Select(e => e.ToResponse());

    public async Task<LetGetResponse> GetByIdAsync(int id) =>
        (await _repo.GetLetByIdAsync(id))?.ToResponse();

    public async Task<LetGetResponse> CreateAsync(LetCreateRequest req)
    {
        var entity = req.ToEntity();
        await _repo.CreateLetAsync(entity);
        return entity.ToResponse();
    }

    public async Task<bool> UpdateAsync(LetUpdateRequest req)
    {
        var existing = await _repo.GetLetByIdAsync(req.BrojLeta);
        if (existing == null) return false;
        req.Map(existing);
        var changed = await _repo.UpdateLetAsync(existing);
        return changed > 0;
    }

    public async Task<bool> DeleteAsync(int id)
        => (await _repo.DeleteLetAsync(id)) > 0;
}