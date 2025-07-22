using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Create;
public class CreateNotesCommandHandler : IRequestHandler<CreateNotesCommand, Note>
{
    private readonly IRepository<Note> _repo;
    public CreateNotesCommandHandler(IRepository<Note> repo)
    {
        _repo = repo;
    }

    public async Task<Note> Handle(CreateNotesCommand request, CancellationToken cancellationToken)
    {
        var note = new Note { Title = request.note.Title, Text = request.note.Text };
        var validator = new NoteValidator(_repo);
        var result = await validator.ValidateAsync(note, options =>
        {
            options.IncludeRuleSets("Default");
        }, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repo.Create(note);
        await _repo.Save();
        return note;
    }
}