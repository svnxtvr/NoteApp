using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Commands.Delete;
public class DeleteRemindersCommandHandler : IRequestHandler<DeleteRemindersCommand>
{
    private readonly IRepository<Reminder> _repo;
    public DeleteRemindersCommandHandler(IRepository<Reminder> repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteRemindersCommand request, CancellationToken cancellationToken)
    {
        await _repo.Delete(request.Title);
        await _repo.Save();
    }
}