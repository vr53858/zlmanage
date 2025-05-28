using Microsoft.EntityFrameworkCore;
using ZLManage.DomainModel.Models;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.Infrastructure.Repositories;

public class ZrakoplovRepository : IZrakoplovRepository
{
    private readonly ZLContext _context;
    
    public ZrakoplovRepository(ZLContext context) => _context = context;

    public async Task<List<Zrakoplov>> GetZrakoploviAsync()
        => await _context.Zrakoplov.ToListAsync();

    public async Task<Zrakoplov> GetZrakoplovByIdAsync(int id)
        => await _context.Zrakoplov.FindAsync(id);

    public async Task<int> CreateZrakoplovAsync(Zrakoplov zrakoplov)
    {
        await _context.Zrakoplov.AddAsync(zrakoplov);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateZrakoplovAsync(Zrakoplov zrakoplov)
    {
        _context.Zrakoplov.Update(zrakoplov);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteZrakoplovAsync(int id)
    {
        var entity = await GetZrakoplovByIdAsync(id);
        if (entity == null) return 0;
        _context.Zrakoplov.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
