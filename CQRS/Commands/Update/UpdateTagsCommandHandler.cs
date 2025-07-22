using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Update;
public class UpdateTagsCommandHandler : IRequestHandler<UpdateTagsCommand, Tag>
{
    private readonly IRepository<Tag> _repo;
    public UpdateTagsCommandHandler(IRepository<Tag> repo)
    {
        _repo = repo;
    }

    public async Task<Tag> Handle(UpdateTagsCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag {NoteTitle = request.tag.NoteTitle, ReminderTitle = request.tag.ReminderTitle };
        var validator = new TagValidator(_repo);
        var result = await validator.ValidateAsync(tag, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repo.Save();
        return tag;
    }
}