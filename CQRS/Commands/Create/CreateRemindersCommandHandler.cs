using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Create;
public class CreateRemindersCommandHandler : IRequestHandler<CreateRemindersCommand, Reminder>
{
    private readonly IRepository<Reminder> _repo;
    public CreateRemindersCommandHandler(IRepository<Reminder> repo)
    {
        _repo = repo;
    }

    public async Task<Reminder> Handle(CreateRemindersCommand request, CancellationToken cancellationToken)
    {
        var reminder = new Reminder { Title = request.reminder.Title, Text = request.reminder.Text, ReminderTime = request.reminder.ReminderTime };
        var validator = new ReminderValidator(_repo);
        var result = await validator.ValidateAsync(reminder, options =>
        {
            options.IncludeRuleSets("Default");
        }, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repo.Create(reminder);
        await _repo.Save();
        return reminder;
    }
}