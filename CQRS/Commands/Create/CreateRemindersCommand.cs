using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Create;
public record CreateRemindersCommand(Reminder reminder) : IRequest<Reminder>;