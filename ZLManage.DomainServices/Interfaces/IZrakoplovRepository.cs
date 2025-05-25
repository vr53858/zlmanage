using ZLManage.DomainModel.Models;

namespace ZLManage.DomainServices.Interfaces;

public interface IZrakoplovRepository
{
    Task<List<Zrakoplov>> GetZrakoploviAsync();
    Task<Zrakoplov> GetZrakoplovByIdAsync(int id);
    Task<int> CreateZrakoplovAsync(Zrakoplov zrakoplov);
    Task<int> UpdateZrakoplovAsync(Zrakoplov zrakoplov);
    Task<int> DeleteZrakoplovAsync(int id);
}
