namespace NoteApp.DB.Entities;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; } = default!;
    public string? NoteTitle { get; set; }
    public Note? Note { get; set; }
    public string? ReminderTitle { get; set; }
    public Reminder? Reminder { get; set; }
}