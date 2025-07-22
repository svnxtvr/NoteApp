using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Queries.Get;
public record GetTagsQuery(string Name) : IRequest<Tag>;