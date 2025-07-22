using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.List;
public class ListNotesQueryHandler : IRequestHandler<ListNotesQuery, List<Note>>
{
    private readonly IRepository<Note> _repo;
    public ListNotesQueryHandler(IRepository<Note> repo)
    {
        _repo = repo;
    }
    public async Task<List<Note>> Handle(ListNotesQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetList();
    }
}