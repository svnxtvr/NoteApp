using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Controllers.CreateControllers;
using NoteApp.DB.Entities;
using NoteApp.CQRS.Commands.Create;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace NoteApp.Tests
{
    public class CreateNotesControllerTests
    {
        [Fact]
        public async Task CreateNote_ReturnsOk_WhenNoteIsValid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new CreateNotesController(mediatorMock.Object);
            var testNote = new Note { Title = "Test", Text = "Test Text", Tags = new List<Tag>() };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateNotesCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testNote);

            // Act
            var result = await controller.CreateNote(testNote);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedNote = Assert.IsType<Note>(okResult.Value);
            Assert.Equal("Test", returnedNote.Title);
        }

        [Fact]
        public async Task CreateNote_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new CreateNotesController(mediatorMock.Object);
            var testNote = new Note { Title = "", Text = "Test text", Tags = new List<Tag>() };

            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Title", "Заголовок не должен быть пустым")
            };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateNotesCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ValidationException(failures));

            // Act
            var result = await controller.CreateNote(testNote);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsAssignableFrom<IEnumerable<object>>(badRequest.Value);
        }
    }
}