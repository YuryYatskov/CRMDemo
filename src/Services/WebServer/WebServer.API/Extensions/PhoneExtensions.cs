namespace WebServer.API.Extensions;

public static class PhoneLiteExtensions
{
    public static List<PhoneLiteDto> ToPhonesLiteDtoList(this List<Phone> phones)
    => phones.Select(phone => DtoFromPhoneLite(phone)).ToList();

    public static PhoneLiteDto ToPhoneLiteDto(this Phone phone)
        => DtoFromPhoneLite(phone);

    private static PhoneLiteDto DtoFromPhoneLite(Phone phone)
        => new()
        {
            Id = phone.Id,
            Number = phone.Number,
            PhoneNoteId = phone.PhoneNoteId,
        };
}

public static class PhoneExtensions
{
    public static IEnumerable<PhoneDto> ToPhonesDtoList(this IEnumerable<Phone> phones)
    => phones.Select(phone => DtoFromPhone(phone));

    public static PhoneDto ToPhoneDto(this Phone phone)
        => DtoFromPhone(phone);

    private static PhoneDto DtoFromPhone(Phone phone)
        => new()
        {
            Id = phone.Id,
            Number = phone.Number,
            PhoneNoteId = phone.PhoneNoteId,
            PhoneNoteName = phone.PhoneNote?.Name,
            CounterpartyId = phone.CounterpartyId,
            CounterpartyName = phone.Counterparty?.Name,
        };
}