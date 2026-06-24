namespace WebServer.API.Endpoints.PhoneNotes.UpdatePhoneNote;

public record UpdatePhoneNoteCommand(PhoneNoteDto PhoneNote) : ICommand<UpdatePhoneNoteResult>;

public record UpdatePhoneNoteResult(bool IsSuccess);

public class UpdatePhoneNoteCommandValidator : AbstractValidator<UpdatePhoneNoteCommand>
{
    public UpdatePhoneNoteCommandValidator()
    {
        RuleFor(x => x.PhoneNote.Name).NotEmpty().WithMessage("Name is required");
        //RuleFor(x => x.PhoneNote.SeparatorId).NotEqual(0).WithMessage("SeparatorId is required");
    }
}

public class UpdateProducHandler(ApplicationDbContext dbContext)
    : ICommandHandler<UpdatePhoneNoteCommand, UpdatePhoneNoteResult>
{
    public async Task<UpdatePhoneNoteResult> Handle(UpdatePhoneNoteCommand command, CancellationToken cancellationToken)
    {
        var phoneNoteId = command.PhoneNote.Id;
        var phoneNote = await dbContext.PhoneNotes
            .FindAsync([phoneNoteId], cancellationToken: cancellationToken)
            ?? throw new PhoneNoteNotFoundException(command.PhoneNote.Id);

        UpdatePhoneNoteWithNewValues(phoneNote, command.PhoneNote);

        dbContext.PhoneNotes.Update(phoneNote);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePhoneNoteResult(true);
    }

    private static void UpdatePhoneNoteWithNewValues(PhoneNote phoneNote, PhoneNoteDto phoneNoteDto)
    {
        phoneNote.Name = phoneNoteDto.Name;
    }
}
