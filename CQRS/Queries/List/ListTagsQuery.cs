using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Queries.List;
public record ListTagsQuery : IRequest<List<Tag>>;