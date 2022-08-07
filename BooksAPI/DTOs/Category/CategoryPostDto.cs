using FluentValidation;

namespace BooksAPI.DTOs.Category
{
    public class CategoryPostDto
    {
        public string Name { get; set; }

    }

    public class CategoryPostDtoValidation : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Fill the field");
        }
    }
}
