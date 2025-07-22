using Microsoft.EntityFrameworkCore;
using NoteApp.DB.Entities;
using NoteApp.DB.Context;

namespace NoteApp.DB.Repository;

public class PostgreSQLNoteRepository : IRepository<Note>
{
    private readonly ApplicationContext db;

    public PostgreSQLNoteRepository()
    {
        this.db = new ApplicationContext();
    }

    public async Task<List<Note>> GetList()
    {
        return await db.Notes
            .Select(n => new Note {NoteId = n.NoteId, Title = n.Title, Text = n.Text, Tags = n.Tags })
            .ToListAsync();
    }

    public async Task<Note?> Get(string Title)
    {
        var note = await db.Notes.Select(n => new Note { NoteId = n.NoteId, Title = n.Title, Text = n.Text, Tags = n.Tags })
            .FirstOrDefaultAsync(n => n.Title == Title);
        if (note == null)
        {
            return null;
        }
        return note;
    }

    public async Task Create(Note note) => await db.Notes.AddAsync(note);

    public async Task Delete(string Title)
    {
        var note = await db.Notes.FirstOrDefaultAsync(n => n.Title == Title);
        if (note == null) return;
        db.Notes.Remove(note);
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