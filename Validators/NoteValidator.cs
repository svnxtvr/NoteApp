using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.Validators;
// Валидатор для класса заметки
public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator(IRepository<Note> repo)
    {
        RuleSet("Default", () =>
        {
            RuleFor(note => note.Title)
                .NotEmpty()
                    .WithMessage("Заголовок не должен быть пустым")
                .MustAsync(async (title, cancellation) =>
                    {
                        var existing = await repo.Get(title);
                        return existing == null;
                    })
                    .WithMessage("Такая заметка уже существует")
                .MaximumLength(20)
                    .WithMessage("Заголовок может иметь не более 20 символов");
            RuleFor(note => note.Text)
                .MaximumLength(640)
                    .WithMessage("Текст должен содержать не более 640 символов");
            RuleFor(note => note.Tags)
                .Must(tags => tags.Select(t => t.Name).Distinct().Count() == tags.Count)
                    .WithMessage("Теги не должны повторяться")
                .ForEach(rule =>
                {
                    rule.Must(tag => !string.IsNullOrWhiteSpace(tag.Name))
                        .WithMessage("Имя тэга не должно быть пустым");
                    rule.MustAsync(async (tag, cancellation) =>
                    {
                        var existingTag = await repo.Get(tag.Name);
                        return existingTag != null;
                    })
                        .WithMessage("Такого тэга не существует");
                });
        });

        RuleSet("Update", () =>
        {
            RuleFor(note => note.Text)
                .MaximumLength(640)
                    .WithMessage("Текст должен содержать не более 640 символов");
            RuleFor(note => note.Tags)
                .Must(tags => tags.Select(t => t.Name).Distinct().Count() == tags.Count)
                    .WithMessage("Теги не должны повторяться")
                .ForEach(rule =>
                {
                    rule.Must(tag => !string.IsNullOrWhiteSpace(tag.Name))
                        .WithMessage("Имя тэга не должно быть пустым");
                    rule.MustAsync(async (tag, cancellation) =>
                    {
                        var existingTag = await repo.Get(tag.Name);
                        return existingTag != null;
                    })
                        .WithMessage("Такого тэга не существует");
                });
        });
    }
}