using MediatR;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.CQRS.Queries.Get;
public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, Tag?>
{
    private readonly IRepository<Tag> _repo;
    public GetTagsQueryHandler(IRepository<Tag> repo)
    {
        _repo = repo;
    }

    public async Task<Tag?> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.Get(request.Name);
    }
}