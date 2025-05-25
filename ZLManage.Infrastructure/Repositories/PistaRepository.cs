using Microsoft.EntityFrameworkCore;
using ZLManage.DomainModel.Models;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.Infrastructure.Repositories;


public class PistaRepository : IPistaRepository
{
    private readonly ZLContext _context;
    public PistaRepository(ZLContext context) => _context = context;

    public async Task<List<Pista>> GetPisteAsync()
        => await _context.Pista.ToListAsync();

    public async Task<Pista> GetPistaByIdAsync(int id)
        => await _context.Pista.FindAsync(id);

    public async Task<int> CreatePistaAsync(Pista pista)
    {
        await _context.Pista.AddAsync(pista);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdatePistaAsync(Pista pista)
    {
        _context.Pista.Update(pista);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeletePistaAsync(int id)
    {
        var entity = await GetPistaByIdAsync(id);
        if (entity == null) return 0;
        _context.Pista.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
