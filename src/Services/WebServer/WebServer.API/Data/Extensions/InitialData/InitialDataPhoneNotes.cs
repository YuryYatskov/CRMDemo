namespace WebServer.API.Data.Extensions;

public static partial class InitialData
{
    public static IEnumerable<PhoneNote> GetPreconfiguredPhoneNotes()
    {
        return
        [
            new PhoneNote()
                {
                    Id = new Guid("600fb3f6-e86b-408f-badb-0c0e1b284a81"),
                    Name = "Personal",
                },
                new PhoneNote()
                {
                    Id = new Guid("6e0783e7-6003-4fac-8590-9e2fe06e4900"),
                    Name = "Work",
                },
        ];
    }
}