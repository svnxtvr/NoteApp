using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Update;
public class UpdateRemindersCommandHandler : IRequestHandler<UpdateRemindersCommand, Reminder>
{
    private readonly IRepository<Reminder> _repo;
    public UpdateRemindersCommandHandler(IRepository<Reminder> repo)
    {
        _repo = repo;
    }

    public async Task<Reminder> Handle(UpdateRemindersCommand request, CancellationToken cancellationToken)
    {
        var reminder = new Reminder {Text = request.reminder.Text, ReminderTime = request.reminder.ReminderTime };
        var validator = new ReminderValidator(_repo);
        var result = await validator.ValidateAsync(reminder, options =>
        {
            options.IncludeRuleSets("Update");
        }, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repo.Save();
        return reminder;
    }
}