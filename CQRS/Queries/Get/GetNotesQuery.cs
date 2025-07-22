using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Queries.Get;
public record GetNotesQuery(string Title) : IRequest<Note>;