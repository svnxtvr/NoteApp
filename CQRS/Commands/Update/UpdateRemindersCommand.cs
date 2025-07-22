using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Update;
public record UpdateRemindersCommand(Reminder reminder) : IRequest<Reminder>;