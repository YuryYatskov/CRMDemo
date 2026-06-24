using CRMWeb.Models.Phones;
using FluentValidation;

namespace CRMWeb.Services.PhoneNotes;

public class PhoneNoteValidator : AbstractValidator<PhoneNoteModel>
{
    public PhoneNoteValidator()
    {
        //RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
