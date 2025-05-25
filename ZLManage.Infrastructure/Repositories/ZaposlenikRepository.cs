using Microsoft.EntityFrameworkCore;
using ZLManage.DomainModel.Models;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.Infrastructure.Repositories;

public class ZaposlenikRepository : IZaposlenikRepository
{
    private readonly ZLContext _context;
    public ZaposlenikRepository(ZLContext context) => _context = context;

    public async Task<List<Zaposlenik>> GetZaposleniciAsync()
        => await _context.Zaposlenik.ToListAsync();

    public async Task<Zaposlenik> GetZaposlenikByIdAsync(int id)
        => await _context.Zaposlenik.FindAsync(id);

    public async Task<int> CreateZaposlenikAsync(Zaposlenik zaposlenik)
    {
        await _context.Zaposlenik.AddAsync(zaposlenik);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateZaposlenikAsync(Zaposlenik zaposlenik)
    {
        _context.Zaposlenik.Update(zaposlenik);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteZaposlenikAsync(int id)
    {
        var entity = await GetZaposlenikByIdAsync(id);
        if (entity == null) return 0;
        _context.Zaposlenik.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
