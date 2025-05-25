using Microsoft.EntityFrameworkCore;
using ZLManage.DomainModel.Models;
using ZLManage.DomainServices.Interfaces;

namespace ZLManage.Infrastructure.Repositories;

public class AdministratorRepository : IAdministratorRepository
{
    private readonly ZLContext _context;
    public AdministratorRepository(ZLContext context) => _context = context;

    public async Task<List<Administrator>> GetAdministratoriAsync()
        => await _context.Administrator.ToListAsync();

    public async Task<Administrator> GetAdministratorByIdAsync(int id)
        => await _context.Administrator.FindAsync(id);

    public async Task<int> CreateAdministratorAsync(Administrator admin)
    {
        await _context.Administrator.AddAsync(admin);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAdministratorAsync(Administrator admin)
    {
        _context.Administrator.Update(admin);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAdministratorAsync(int id)
    {
        var entity = await GetAdministratorByIdAsync(id);
        if (entity == null) return 0;
        _context.Administrator.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
