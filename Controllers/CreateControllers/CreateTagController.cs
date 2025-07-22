using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using NoteApp.CQRS.Commands.Create;
using NoteApp.DB.Entities;

namespace NoteApp.Controllers.CreateControllers;
[Route("/api/v1/function/tag/create")]
    [ApiController]
    public class CreateTagsController : Controller
    {
        private readonly IMediator _mediator;
        public CreateTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Tag tag)
        {
            try
            {
                var tagToReturn = await _mediator.Send(new CreateTagsCommand(tag));
                return Ok(tagToReturn);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }
        }
    }