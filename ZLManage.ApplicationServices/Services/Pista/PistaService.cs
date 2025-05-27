using ZLManage.ApplicationServices.Mappers;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.ApplicationServices.Services.Pista;

public class PistaService : IPistaService
{
    private readonly IPistaRepository _repo;
    public PistaService(IPistaRepository repo) => _repo = repo;

    public async Task<IEnumerable<PistaGetResponse>> GetAllAsync() =>
        (await _repo.GetPisteAsync()).Select(e => e.ToResponse());

    public async Task<PistaGetResponse> GetByIdAsync(int id) =>
        (await _repo.GetPistaByIdAsync(id))?.ToResponse();

    public async Task<PistaGetResponse> CreateAsync(PistaCreateRequest r)
    {
        var e = r.ToEntity();
        await _repo.CreatePistaAsync(e);
        return e.ToResponse();
    }

    public async Task<bool> UpdateAsync(PistaUpdateRequest r)
    {
        var e = await _repo.GetPistaByIdAsync(r.IdPiste);
        if (e == null) return false;
        r.Map(e);
        return (await _repo.UpdatePistaAsync(e)) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
        => (await _repo.DeletePistaAsync(id)) > 0;
}