namespace WebServer.API.Endpoints.PhoneNotes.DeletePhoneNote;

public record DeletePhoneNoteCommand(Guid PhoneNoteId)
    : ICommand<DeletePhoneNoteResult>;

public record DeletePhoneNoteResult(bool IsSuccess);

public class DeletePhoneNoteCommandValidator : AbstractValidator<DeletePhoneNoteCommand>
{
    public DeletePhoneNoteCommandValidator()
    {
        RuleFor(x => x.PhoneNoteId).NotEmpty().WithMessage("PhoneNoteId is required");
    }
}

public class DeletePhoneNoteHandler(ApplicationDbContext dbContext)
    : ICommandHandler<DeletePhoneNoteCommand, DeletePhoneNoteResult>
{
    public async Task<DeletePhoneNoteResult> Handle(DeletePhoneNoteCommand command, CancellationToken cancellationToken)
    {
        // Delete PhoneNote entity from command object.
        var phoneNoteId = command.PhoneNoteId;
        var phoneNote = await dbContext.PhoneNotes
            .FindAsync([phoneNoteId], cancellationToken: cancellationToken)
            ?? throw new PhoneNoteNotFoundException(command.PhoneNoteId);


        // TODO: Чи не силається на інші елементи.
        dbContext.PhoneNotes.Remove(phoneNote);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePhoneNoteResult(true);
    }
}
