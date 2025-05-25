using ZLManage.DomainModel.Models;

namespace ZLManage.DomainServices.Interfaces;

public interface IPistaRepository
{
    Task<List<Pista>> GetPisteAsync();
    Task<Pista> GetPistaByIdAsync(int id);
    Task<int> CreatePistaAsync(Pista pista);
    Task<int> UpdatePistaAsync(Pista pista);
    Task<int> DeletePistaAsync(int id);
}
