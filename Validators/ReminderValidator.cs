using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.Validators;
// Валидатор для класса напоминания
public class ReminderValidator : AbstractValidator<Reminder>
{
    public ReminderValidator(IRepository<Reminder> repo)
    {
        RuleSet("Default", () =>
        {
            RuleFor(reminder => reminder.Title)
                .NotEmpty()
                    .WithMessage("Заголовок не должен быть пустым")
                .MustAsync(async (title, cancellation) =>
                    {
                        var existing = await repo.Get(title);
                        return existing == null;
                    })
                    .WithMessage("Такое напоминание уже существует")
                .MaximumLength(20)
                    .WithMessage("Заголовок может иметь не более 20 символов");
            RuleFor(reminder => reminder.Text)
                .MaximumLength(640)
                    .WithMessage("Текст должен содержать не более 640 символов");
            RuleFor(reminder => reminder.ReminderTime)
                .GreaterThan(DateTime.Now)
                    .WithMessage("Дата и время должно быть позже текущего");
            RuleFor(note => note.Tags)
                .ForEach(rule =>
                {
                    rule.Must(tag => tag.Name != "")
                        .WithMessage("Имя тэга не должно быть пустым");
                    rule.Must(tag => tag.Name != tag.Name)
                        .WithMessage("Такого тэга не существует");
                });
        });

        RuleSet("Update", () =>
        {
            RuleFor(reminder => reminder.Text)
                .MaximumLength(640)
                    .WithMessage("Текст должен содержать не более 640 символов");
            RuleFor(reminder => reminder.ReminderTime)
                .GreaterThan(DateTime.Now)
                    .WithMessage("Дата и время должно быть позже текущего");
            RuleFor(note => note.Tags)
                .ForEach(rule =>
                {
                    rule.Must(tag => tag.Name != "")
                        .WithMessage("Имя тэга не должно быть пустым");
                    rule.Must(tag => tag.Name != tag.Name)
                        .WithMessage("Такого тэга не существует");
                });
        });
    } 
}