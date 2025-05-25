using Microsoft.EntityFrameworkCore;
using ZLManage.DomainModel.Models;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.Infrastructure.Repositories;


public class LetRepository : ILetRepository
{
    private readonly ZLContext _context;
    public LetRepository(ZLContext context) => _context = context;

    public async Task<List<Let>> GetLetoviAsync()
        => await _context.Let.ToListAsync();

    public async Task<Let> GetLetByIdAsync(int id)
        => await _context.Let.FindAsync(id);

    public async Task<int> CreateLetAsync(Let let)
    {
        await _context.Let.AddAsync(let);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateLetAsync(Let let)
    {
        _context.Let.Update(let);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteLetAsync(int id)
    {
        var entity = await GetLetByIdAsync(id);
        if (entity == null) return 0;
        _context.Let.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
