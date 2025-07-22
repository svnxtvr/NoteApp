using MediatR;
using NoteApp.DB.Entities;

namespace NoteApp.CQRS.Commands.Update;
public record UpdateTagsCommand(Tag tag) : IRequest<Tag>;