using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.List;

namespace NoteApp.Controllers.ListControllers;
[Route("/api/v1/function/tag/get-all")]
[ApiController]
public class ListTagsController : Controller
{
    private readonly IMediator _mediator;
    public ListTagsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> GetAllTags()
    {
        var result = await _mediator.Send(new ListTagsQuery());
        return Ok(result);
    }
}