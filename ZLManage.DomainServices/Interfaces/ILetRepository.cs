using ZLManage.DomainModel.Models;

namespace ZLManage.DomainServices.Interfaces;

public interface ILetRepository
{
    Task<List<Let>> GetLetoviAsync();
    Task<Let> GetLetByIdAsync(int id);
    Task<int> CreateLetAsync(Let let);
    Task<int> UpdateLetAsync(Let let);
    Task<int> DeleteLetAsync(int id);
}