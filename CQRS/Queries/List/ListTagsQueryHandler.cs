using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.List;
public class ListTagsQueryHandler : IRequestHandler<ListTagsQuery, List<Tag>>
{
    private readonly IRepository<Tag> _repo;
    public ListTagsQueryHandler(IRepository<Tag> repo)
    {
        _repo = repo;
    }
    public async Task<List<Tag>> Handle(ListTagsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetList();
    }
}