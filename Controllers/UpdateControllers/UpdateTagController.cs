using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Update;
using NoteApp.CQRS.Queries.Get;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.UpdateControllers;
[Route("/api/v1/function/tag/update")]
    [ApiController]
    public class UpdateTagsController : Controller
    {
        private readonly IMediator _mediator;
        public UpdateTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> UpdateTag(string name, [FromBody] Tag tag)
        {
            try
            {
                var getTag = await _mediator.Send(new GetTagsQuery(name));
                if (getTag == null) return NotFound("Тэг не найден");
                getTag.NoteTitle = tag.NoteTitle;
                getTag.ReminderTitle = tag.ReminderTitle;
                var tagToReturn = await _mediator.Send(new UpdateTagsCommand(getTag));
                return Ok(tagToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }