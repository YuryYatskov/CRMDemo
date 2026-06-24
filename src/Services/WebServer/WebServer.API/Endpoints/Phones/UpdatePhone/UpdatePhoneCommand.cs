namespace WebServer.API.Endpoints.Phones.UpdatePhone;

public record UpdatePhoneCommand(PhoneDto Phone) : ICommand<UpdatePhoneResult>;

public record UpdatePhoneResult(bool IsSuccess);

public class UpdatePhoneCommandValidator : AbstractValidator<UpdatePhoneCommand>
{
    public UpdatePhoneCommandValidator()
    {
        RuleFor(x => x.Phone.Number).NotEmpty().WithMessage("Name is required");
    }
}

public class UpdateProducHandler(ApplicationDbContext dbContext)
    : ICommandHandler<UpdatePhoneCommand, UpdatePhoneResult>
{
    public async Task<UpdatePhoneResult> Handle(UpdatePhoneCommand command, CancellationToken cancellationToken)
    {
        var phoneId = command.Phone.Id;
        var phone = await dbContext.Phones
            .FindAsync([phoneId], cancellationToken: cancellationToken)
            ?? throw new PhoneNotFoundException(command.Phone.Id);

        UpdatePhoneWithNewValues(dbContext, phone, command.Phone);

        dbContext.Phones.Update(phone);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePhoneResult(true);
    }

    private static void UpdatePhoneWithNewValues(ApplicationDbContext dbContext, Phone phone, PhoneDto phoneDto)
    {
        var phoneNoteId = phoneDto.PhoneNoteId;
        var phoneNote = new PhoneNote { Id = phoneNoteId };
        dbContext.Entry(phoneNote).State = EntityState.Unchanged;

        var counterpartyId = phoneDto.CounterpartyId;
        var counterparty = counterpartyId != null ? new Counterparty { Id = counterpartyId.Value } : null;
        if (counterparty != null) dbContext.Entry(counterparty).State = EntityState.Unchanged;

        phone.Number = phoneDto.Number;
        phone.PhoneNoteId = phoneNoteId;
        phone.PhoneNote = phoneNote;
        phone.CounterpartyId = phoneDto.CounterpartyId;
        phone.Counterparty = counterparty;
    }
}