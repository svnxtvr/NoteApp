using MediatR;

namespace NoteApp.CQRS.Commands.Delete;
public record DeleteNotesCommand(string Title) : IRequest;