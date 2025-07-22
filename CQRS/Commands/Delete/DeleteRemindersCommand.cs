using MediatR;

namespace NoteApp.CQRS.Commands.Delete;
public record DeleteRemindersCommand(string Title) : IRequest;