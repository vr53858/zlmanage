using ZLManage.DomainModel.Models;

namespace ZLManage.DomainServices.Interfaces;

public interface IZaposlenikRepository
{
    Task<List<Zaposlenik>> GetZaposleniciAsync();
    Task<Zaposlenik> GetZaposlenikByIdAsync(int id);
    Task<int> CreateZaposlenikAsync(Zaposlenik zaposlenik);
    Task<int> UpdateZaposlenikAsync(Zaposlenik zaposlenik);
    Task<int> DeleteZaposlenikAsync(int id);
}
