using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.List;
public class ListRemindersQueryHandler : IRequestHandler<ListRemindersQuery, List<Reminder>>
{
    private readonly IRepository<Reminder> _repo;
    public ListRemindersQueryHandler(IRepository<Reminder> repo)
    {
        _repo = repo;
    }
    public async Task<List<Reminder>> Handle(ListRemindersQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetList();
    }
}