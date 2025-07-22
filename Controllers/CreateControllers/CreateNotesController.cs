using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Create;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.CreateControllers;
[Route("/api/v1/function/note/create")]
    [ApiController]
    public class CreateNotesController : Controller
    {
        private readonly IMediator _mediator;
        public CreateNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            try
            {
                var noteToReturn = await _mediator.Send(new CreateNotesCommand(note));
                return Ok(noteToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }