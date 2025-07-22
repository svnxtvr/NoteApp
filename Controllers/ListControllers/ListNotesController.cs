using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.List;

namespace NoteApp.Controllers.ListControllers;
[Route("/api/v1/function/note/get-all")]
[ApiController]
public class ListNotesController : Controller
{
    private readonly IMediator _mediator;
    public ListNotesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> GetAllNotes()
    {
        var result = await _mediator.Send(new ListNotesQuery());
        return Ok(result);
    }
}