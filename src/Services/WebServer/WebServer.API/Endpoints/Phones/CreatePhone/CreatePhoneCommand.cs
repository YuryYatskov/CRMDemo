namespace WebServer.API.Endpoints.Phones.CreatePhone;

public record CreatePhoneCommand(PhoneDto Phone)
    : ICommand<CreatePhoneResult>;

public record CreatePhoneResult(Guid Id);

public class CreatePhoneCommandValidator : AbstractValidator<CreatePhoneCommand>
{
    public CreatePhoneCommandValidator()
    {
        RuleFor(x => x.Phone.Number).NotEmpty().WithMessage("Name is required");
    }
}

public class CreatePhoneHandler(ApplicationDbContext dbContext)
    : ICommandHandler<CreatePhoneCommand, CreatePhoneResult>
{
    public async Task<CreatePhoneResult> Handle(CreatePhoneCommand command, CancellationToken cancellationToken)
    {
        var phoneNoteId = command.Phone.PhoneNoteId;
        var phoneNote = new PhoneNote { Id = phoneNoteId };
        dbContext.Entry(phoneNote).State = EntityState.Unchanged;

        var counterpartyId = command.Phone.CounterpartyId;
        var counterparty = counterpartyId != null ? new Counterparty { Id = counterpartyId.Value } : null;
        if (counterparty != null) dbContext.Entry(counterparty).State = EntityState.Unchanged;

        Phone phone = new()
        {
            Id = Guid.NewGuid(),
            Number = command.Phone.Number,
            PhoneNoteId = phoneNoteId,
            PhoneNote = phoneNote,
            CounterpartyId = counterpartyId,
            Counterparty = counterparty,
        };

        dbContext.Phones.Add(phone);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePhoneResult(phone.Id);
    }
}
