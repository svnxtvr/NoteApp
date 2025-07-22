using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Update;
using NoteApp.CQRS.Queries.Get;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.UpdateControllers;
[Route("/api/v1/function/reminder/update")]
    [ApiController]
    public class UpdateRemindersController : Controller
    {
        private readonly IMediator _mediator;
        public UpdateRemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> UpdateReminder(string title, [FromBody] Reminder reminder)
        {
            try
            {
                var getReminder = await _mediator.Send(new GetRemindersQuery(title));
                if (getReminder == null) return NotFound("Напоминание не найдено");
                getReminder.Text = reminder.Text;
                getReminder.ReminderTime = reminder.ReminderTime;
                var reminderToReturn = await _mediator.Send(new UpdateRemindersCommand(getReminder));
                return Ok(reminderToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }