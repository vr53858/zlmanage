using ZLManage.DomainModel.Models;

namespace ZLManage.DomainServices.Interfaces;

public interface IAdministratorRepository
{
    Task<List<Administrator>> GetAdministratoriAsync();
    Task<Administrator> GetAdministratorByIdAsync(int id);
    Task<int> CreateAdministratorAsync(Administrator admin);
    Task<int> UpdateAdministratorAsync(Administrator admin);
    Task<int> DeleteAdministratorAsync(int id);
}
