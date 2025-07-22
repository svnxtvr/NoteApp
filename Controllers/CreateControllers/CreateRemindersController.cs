using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Create;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.CreateControllers;
[Route("/api/v1/function/reminder/create")]
    [ApiController]
    public class CreateRemindersController : Controller
    {
        private readonly IMediator _mediator;
        public CreateRemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder([FromBody] Reminder reminder)
        {
            try
            {
                var reminderToReturn = await _mediator.Send(new CreateRemindersCommand(reminder));
                return Ok(reminderToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }