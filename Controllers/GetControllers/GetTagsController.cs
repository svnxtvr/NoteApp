using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.GetControllers;
[Route("/api/v1/function/tag/get")]
    [ApiController]
    public class GetTagsController : Controller
    {
        private readonly IMediator _mediator;
        public GetTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> GetTagByName(string name)
        {
            var tag = await _mediator.Send(new GetTagsQuery(name));
            if (tag == null) return NotFound("Тэг не найден");
            return Ok(tag);
        }
    }