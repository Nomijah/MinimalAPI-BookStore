using FluentValidation;
using Labb1.Models.DTOs;

namespace Labb1.Models.Validations
{
    public class BookPostValidation : AbstractValidator<BookCreateDTO>
    {
        public BookPostValidation() 
        {
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
            RuleFor(model => model.Year).LessThan(Convert.ToInt32(DateTime.Now.Year + 1));
        }
    }
}
