using FluentValidation;
using NoteApp.DB.Entities;
using NoteApp.DB.Repository;

namespace NoteApp.Validators;
// Валидатор для класса тэг
public class TagValidator : AbstractValidator<Tag>
{
    public TagValidator(IRepository<Tag> repo)
    {
        RuleFor(tag => tag.Name)
            .NotEmpty()
                .WithMessage("Имя тэга не должно быть пустым")
            .MustAsync(async (name, cancellation) =>
                {
                    var existing = await repo.Get(name);
                    return existing == null;
                })
                .WithMessage("Такой тэг уже существует")
            .MaximumLength(20)
                .WithMessage("Имя тэга может иметь не более 20 символов");
    } 
}