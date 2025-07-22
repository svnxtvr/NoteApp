using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.Get;
public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, Note?>
{
    private readonly IRepository<Note> _repo;
    public GetNotesQueryHandler(IRepository<Note> repo)
    {
        _repo = repo;
    }

    public async Task<Note?> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        return await _repo.Get(request.Title);
    }
}