namespace NoteApp.DB.Entities;

public class Reminder
{
    public int ReminderId { get; set; }
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;
    public DateTime? ReminderTime { get; set; }
    public List<Tag> Tags { get; set; } = new();
}