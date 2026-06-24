namespace WebServer.API.Models;

public class Phone
{
    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public Guid PhoneNoteId { get; set; } = default!;
    public PhoneNote? PhoneNote { get; set; }

    public Guid? CounterpartyId { get; set; }
    public Counterparty? Counterparty { get; set; }
}
