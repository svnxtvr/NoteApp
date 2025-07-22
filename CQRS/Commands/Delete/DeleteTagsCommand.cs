using MediatR;

namespace NoteApp.CQRS.Commands.Delete;
public record DeleteTagsCommand(string Name) : IRequest;