using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.GetControllers;
[Route("/api/v1/function/note/get")]
    [ApiController]
    public class GetNotesController : Controller
    {
        private readonly IMediator _mediator;
        public GetNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> GetNoteByName(string title)
        {
            var note = await _mediator.Send(new GetNotesQuery(title));
            if (note == null) return NotFound("Заметка не найдена");
            return Ok(note);
        }
    }