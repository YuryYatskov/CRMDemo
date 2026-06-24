namespace BuildingBlocks.DTO.Dtos;

public class PhoneLiteDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public Guid PhoneNoteId { get; set; }
}

public class PhoneDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public Guid PhoneNoteId { get; set; }
    public string? PhoneNoteName { get; set; }
    public Guid? CounterpartyId { get; set; }
    public string? CounterpartyName { get; set; }
}
