using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Update;
public class UpdateNotesCommandHandler : IRequestHandler<UpdateNotesCommand, Note>
{
    private readonly IRepository<Note> _repo;
    public UpdateNotesCommandHandler(IRepository<Note> repo)
    {
        _repo = repo;
    }

    public async Task<Note> Handle(UpdateNotesCommand request, CancellationToken cancellationToken)
    {
        var note = new Note {Text = request.note.Text};
        var validator = new NoteValidator(_repo);
        var result = await validator.ValidateAsync(note, options =>
        {
            options.IncludeRuleSets("Update");
        }, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repo.Save();
        return note;
    }
}