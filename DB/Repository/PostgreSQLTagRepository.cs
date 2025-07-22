using NoteApp.DB.Entities;
using NoteApp.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.DB.Repository;
public class PostgreSQLTagRepository : IRepository<Tag>
{
    private readonly ApplicationContext db;

    public PostgreSQLTagRepository()
    {
        this.db = new ApplicationContext();
    }

    public async Task<List<Tag>> GetList()
    {
        return await db.Tags
            .Select(t => new Tag {TagId = t.TagId, Name = t.Name, NoteTitle = t.NoteTitle, Note = t.Note, ReminderTitle = t.ReminderTitle, Reminder = t.Reminder })
            .ToListAsync();
    }

    public async Task<Tag?> Get(string Name)
    {

        var tag = await db.Tags
            .Select(t => new Tag {TagId = t.TagId, Name = t.Name, NoteTitle = t.NoteTitle, Note = t.Note, ReminderTitle = t.ReminderTitle, Reminder = t.Reminder })
            .FirstOrDefaultAsync(t => t.Name == Name);
        if (tag == null)
        {
            return null;
        }
        return tag;
    }

    public async Task Create(Tag tag)
    {
        await db.Tags.AddAsync(tag);
    }

    public async Task Delete(string Name)
    {
        var tag = await db.Tags.FirstOrDefaultAsync(t => t.Name == Name);
        if (tag == null) return;
        db.Tags.Remove(tag);
    }

    public async Task Save() => await db.SaveChangesAsync();

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}