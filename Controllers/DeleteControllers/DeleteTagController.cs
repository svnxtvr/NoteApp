using Microsoft.AspNetCore.Mvc;
using MediatR;
using NoteApp.CQRS.Commands.Delete;
using NoteApp.CQRS.Queries.Get;

namespace NoteApp.Controllers.DeleteControllers;
[Route("/api/v1/function/tag/delete")]
    [ApiController]
    public class DeleteTagsController : Controller
    {
        private readonly IMediator _mediator;
        public DeleteTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> DeleteTag(string name)
        {
            var getTag = await _mediator.Send(new GetTagsQuery(name));
            if (getTag == null) return NotFound($"Тэг '{name}' не найден");
            await _mediator.Send(new DeleteTagsCommand(name));
            return Ok($"Тэг '{name}' удален.");
        }
    }