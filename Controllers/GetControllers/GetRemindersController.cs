using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.GetControllers;
[Route("/api/v1/function/reminder/get")]
    [ApiController]
    public class GetRemindersController : Controller
    {
        private readonly IMediator _mediator;
        public GetRemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> GetReminderByName(string title)
        {
            var reminder = await _mediator.Send(new GetRemindersQuery(title));
            if (reminder == null) return NotFound("Напоминание не найдено");
            return Ok(reminder);
        }
    }