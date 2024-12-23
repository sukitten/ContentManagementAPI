using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Content
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class Media
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Content> Contents { get; set; }
    public DbSet<Media> Medias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("YourConnectionString"));
    }
}

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _dbSet.FindAsync(id);
        if (item != null)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

public class ContentRepository : Repository<Content>
{
    public ContentRepository(ApplicationDbContext context) : base(context) {}
}

public class MediaRepository : Repository<Media>
{
    public MediaRepository(ApplicationDbContext context) : base(context) {}
}