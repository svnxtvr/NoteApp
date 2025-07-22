using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Commands.Delete;
public class DeleteTagsCommandHandler : IRequestHandler<DeleteTagsCommand>
{
    private readonly IRepository<Tag> _repo;
    public DeleteTagsCommandHandler(IRepository<Tag> repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteTagsCommand request, CancellationToken cancellationToken)
    {
        await _repo.Delete(request.Name);
        await _repo.Save();
    }
}