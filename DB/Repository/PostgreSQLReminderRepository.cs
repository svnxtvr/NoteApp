using NoteApp.DB.Entities;
using NoteApp.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.DB.Repository;

public class PostgreSQLReminderRepository : IRepository<Reminder>
{
    private readonly ApplicationContext db;

    public PostgreSQLReminderRepository()
    {
        this.db = new ApplicationContext();
    }

    public async Task<List<Reminder>> GetList()
    {
        return await db.Reminders
            .Select(r => new Reminder {ReminderId = r.ReminderId, Title = r.Title, Text = r.Text, ReminderTime = r.ReminderTime, Tags = r.Tags })
            .ToListAsync();
    }

    public async Task<Reminder?> Get(string Title)
    {
        var reminder = await db.Reminders
            .Select(r => new Reminder {ReminderId = r.ReminderId, Title = r.Title, Text = r.Text, ReminderTime = r.ReminderTime, Tags = r.Tags })
            .FirstOrDefaultAsync(r => r.Title == Title);
        if (reminder == null)
        {
            return null;
        }
        return reminder;
    }

    public async Task Create(Reminder reminder) => await db.Reminders.AddAsync(reminder);

    public async Task Delete(string Title)
    {
        var reminder = await db.Reminders.FirstOrDefaultAsync(r => r.Title == Title);
        if (reminder == null) return;
        db.Reminders.Remove(reminder);
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