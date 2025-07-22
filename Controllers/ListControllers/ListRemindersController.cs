using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.List;

namespace NoteApp.Controllers.ListControllers;
[Route("/api/v1/function/reminder/get-all")]
[ApiController]
public class ListRemindersController : Controller
{
    private readonly IMediator _mediator;
    public ListRemindersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> GetAllReminders()
    {
        var result = await _mediator.Send(new ListRemindersQuery());
        return Ok(result);
    }
}