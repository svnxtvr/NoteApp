namespace NoteApp.DB.Entities;

public class Note
{
    public int NoteId { get; set; }
    public string Title { get; set; } = default!;
    public string Text { get; set; } = default!;
    public List<Tag> Tags { get; set; } = new();
}