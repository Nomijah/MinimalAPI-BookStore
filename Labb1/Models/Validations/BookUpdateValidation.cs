using FluentValidation;

namespace Labb1.Models.Validations
{
    public class BookUpdateValidation : AbstractValidator<Book>
    {
        public BookUpdateValidation() 
        {
            RuleFor(model => model.Id).NotEmpty();
        }
    }
}
