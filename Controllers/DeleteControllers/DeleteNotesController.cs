using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Commands.Delete;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.DeleteControllers;
[Route("/api/v1/function/note/delete")]
    [ApiController]
    public class DeleteNotesController : Controller
    {
        private readonly IMediator _mediator;
        public DeleteNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> DeleteNote(string title)
        {
            var getNote = await _mediator.Send(new GetNotesQuery(title));
            if (getNote == null) return NotFound($"Заметка '{title}' не найдена");
            await _mediator.Send(new DeleteNotesCommand(title));
            return Ok($"Заметка '{title}' удалена.");
        }
    }