using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Queries.Get;
public record GetRemindersQuery(string Title) : IRequest<Reminder>;