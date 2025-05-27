using ZLManage.ApplicationServices.Mappers;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.ApplicationServices.Services.Administrator;

public class AdministratorService : IAdministratorService
{
    private readonly IAdministratorRepository _repo;
    public AdministratorService(IAdministratorRepository repo) => _repo = repo;

    public async Task<IEnumerable<AdministratorGetResponse>> GetAllAsync() =>
        (await _repo.GetAdministratoriAsync()).Select(a => a.ToResponse());

    public async Task<AdministratorGetResponse> GetByIdAsync(int id) =>
        (await _repo.GetAdministratorByIdAsync(id))?.ToResponse();

    public async Task<AdministratorGetResponse> CreateAsync(AdministratorCreateRequest r)
    {
        var e = r.ToEntity();
        await _repo.CreateAdministratorAsync(e);
        return e.ToResponse();
    }

    public async Task<bool> UpdateAsync(AdministratorUpdateRequest r)
    {
        var e = await _repo.GetAdministratorByIdAsync(r.AdminId);
        if (e == null) return false;
        r.Map(e);
        return (await _repo.UpdateAdministratorAsync(e)) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
        => (await _repo.DeleteAdministratorAsync(id)) > 0;
}