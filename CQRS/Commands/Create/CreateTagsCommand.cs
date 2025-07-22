using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Create;
public record CreateTagsCommand(Tag tag) : IRequest<Tag>;