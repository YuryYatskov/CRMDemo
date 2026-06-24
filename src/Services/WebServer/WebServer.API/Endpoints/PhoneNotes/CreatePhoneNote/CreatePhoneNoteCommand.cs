namespace WebServer.API.Endpoints.PhoneNotes.CreatePhoneNote;

public record CreatePhoneNoteCommand(PhoneNoteDto PhoneNote)
    : ICommand<CreatePhoneNoteResult>;

public record CreatePhoneNoteResult(Guid Id);

public class CreatePhoneNoteCommandValidator : AbstractValidator<CreatePhoneNoteCommand>
{
    public CreatePhoneNoteCommandValidator()
    {
        RuleFor(x => x.PhoneNote.Name).NotEmpty().WithMessage("Name is required");
        //RuleFor(x => x.PhoneNote.SeparatorId).NotEqual(0).WithMessage("SeparatorId is required");
    }
}

public class CreatePhoneNoteHandler(ApplicationDbContext dbContext)
    : ICommandHandler<CreatePhoneNoteCommand, CreatePhoneNoteResult>
{
    public async Task<CreatePhoneNoteResult> Handle(CreatePhoneNoteCommand command, CancellationToken cancellationToken)
    {
        PhoneNote phoneNote = new()
        {
            Id = Guid.NewGuid(),
            Name = command.PhoneNote.Name
        };

        dbContext.PhoneNotes.Add(phoneNote);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePhoneNoteResult(phoneNote.Id);
    }
}
