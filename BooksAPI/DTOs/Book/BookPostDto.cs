using FluentValidation;
using System;

namespace BooksAPI.DTOs.Book
{
    public class BookPostDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Author { get; set; }
        public DateTime releaseDate { get; set; }
        public short Pages { get; set; }
    }

    public class BookPostDtoValidation : AbstractValidator<BookPostDto>
    {
        public BookPostDtoValidation()
        {
            RuleFor(b => b.Name).NotNull().WithMessage("Name is required").MaximumLength(30).WithMessage("Name's max length must be less  than 30");
            RuleFor(b => b.Author).NotNull().WithMessage("Author name is required").MaximumLength(50).WithMessage("Name's max length must be less  than 30");
            RuleFor(b => b.CategoryId).NotEmpty();
        }
    }
}
