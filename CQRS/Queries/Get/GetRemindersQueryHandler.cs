using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.Get;
public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, Reminder?>
{
    private readonly IRepository<Reminder> _repo;
    public GetRemindersQueryHandler(IRepository<Reminder> repo)
    {
        _repo = repo;
    }

    public async Task<Reminder?> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        return await _repo.Get(request.Title);
    }
}