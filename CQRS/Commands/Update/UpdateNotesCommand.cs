using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Update;
public record UpdateNotesCommand(Note note) : IRequest<Note>;