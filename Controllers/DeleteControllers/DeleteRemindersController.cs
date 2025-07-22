using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Commands.Delete;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.DeleteControllers;
[Route("/api/v1/function/reminder/delete")]
    [ApiController]
    public class DeleteRemindersController : Controller
    {
        private readonly IMediator _mediator;
        public DeleteRemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> DeleteReminder(string title)
        {
            var getReminder = await _mediator.Send(new GetRemindersQuery(title));
            if (getReminder == null) return NotFound($"Напоминание '{title}' не найдено");
            await _mediator.Send(new DeleteRemindersCommand(title));
            return Ok($"Напоминание '{title}' удалено.");
        }
    }