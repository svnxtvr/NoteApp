using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Update;
using NoteApp.CQRS.Queries.Get;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.UpdateControllers;
[Route("/api/v1/function/note/update")]
    [ApiController]
    public class UpdateNotesController : Controller
    {
        private readonly IMediator _mediator;
        public UpdateNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> UpdateNote(string title, [FromBody] Note note)
        {
            try
            {
                var getNote = await _mediator.Send(new GetNotesQuery(title));
                if (getNote == null) return NotFound("Заметка не найдена");
                getNote.Text = note.Text;
                var noteToReturn = await _mediator.Send(new UpdateNotesCommand(getNote));
                return Ok(noteToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }