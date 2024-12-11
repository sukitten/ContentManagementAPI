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

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class ContentRepository : IRepository<Content>
{
    private readonly ApplicationDbContext _context;

    public ContentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Content>> GetAllAsync()
    {
        return await _context.Contents.ToListAsync();
    }

    public async Task<Content> GetByIdAsync(int id)
    {
        return await _context.Contents.FindAsync(id);
    }

    public async Task InsertAsync(Content entity)
    {
        _context.Contents.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Content entity)
    {
        _context.Contents.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Contents.FindAsync(id);
        if (item != null)
        {
            _context.Contents.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

public class MediaRepository : IRepository<Media>
{
    private readonly ApplicationDbContext _context;

    public MediaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Media>> GetAllAsync()
    {
        return await _context.Medias.ToListAsync();
    }

    public async Task<Media> GetByIdAsync(int id)
    {
        return await _context.Medias.FindAsync(id);
    }

    public async Task InsertAsync(Media entity)
    {
        _context.Medias.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Media entity)
    {
        _context.Medias.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Medias.FindAsync(id);
        if (item != null)
        {
            _context.Medias.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}