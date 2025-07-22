using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Commands.Delete;
public class DeleteNotesCommandHandler : IRequestHandler<DeleteNotesCommand>
{
    private readonly IRepository<Note> _repo;
    public DeleteNotesCommandHandler(IRepository<Note> repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteNotesCommand request, CancellationToken cancellationToken)
    {
        await _repo.Delete(request.Title);
        await _repo.Save();
    }
}