using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Create;
public record CreateNotesCommand(Note note) : IRequest<Note>;