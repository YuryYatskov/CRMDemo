namespace WebServer.API.Extensions;

public static class PhoneNoteExtensions
{
    public static IEnumerable<PhoneNoteDto> ToPhoneNotesDtoList(this IEnumerable<PhoneNote> phoneNotes)
    => phoneNotes.Select(phoneNote => DtoFromPhoneNote(phoneNote));

    public static PhoneNoteDto ToPhoneNoteDto(this PhoneNote phoneNote)
        => DtoFromPhoneNote(phoneNote);

    private static PhoneNoteDto DtoFromPhoneNote(PhoneNote phoneNote)
        => new() { 
            Id = phoneNote.Id,
            Name = phoneNote.Name,
        };
}
