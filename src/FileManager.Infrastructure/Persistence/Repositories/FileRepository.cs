using File = FileManager.Domain.Entities.File;

namespace FileManager.Infrastructure.Persistence.Repositories;

public class FileRepository : IRepository<File>
{
    private readonly ApplicationDbContext _context;

    public FileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<File>> GetAllAsync()
    {
        return await _context.Files.ToListAsync();
    }

    public async Task<File?> GetByIdAsync(int id)
    {
        return await _context.Files.FindAsync(id);
    }

    public async Task<File?> GetAsync(Func<File, bool> predicate)
    {
        return await Task.Run(() => _context.Files.AsQueryable().FirstOrDefault(predicate));
    }

    public async Task AddAsync(File entity)
    {
        await _context.Files.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(File entity)
    {
        _context.Files.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file != null)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}
