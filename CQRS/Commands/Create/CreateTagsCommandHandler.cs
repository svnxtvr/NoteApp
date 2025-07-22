using MediatR;
using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;
using NoteApp.Validators;

namespace NoteApp.CQRS.Commands.Create;
public class CreateTagsCommandHandler : IRequestHandler<CreateTagsCommand, Tag>
{
    private readonly IRepository<Tag> _repoTag;
    public CreateTagsCommandHandler(IRepository<Tag> repoTag)
    {
        _repoTag = repoTag;
    }

    public async Task<Tag> Handle(CreateTagsCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag { Name = request.tag.Name, NoteTitle = request.tag.NoteTitle, ReminderTitle = request.tag.ReminderTitle };
        var validator = new TagValidator(_repoTag);
        var result = await validator.ValidateAsync(tag, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        await _repoTag.Create(tag);
        await _repoTag.Save();
        return tag;
    }
}