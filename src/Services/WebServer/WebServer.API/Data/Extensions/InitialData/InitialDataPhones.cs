namespace WebServer.API.Data.Extensions;

public static partial class InitialData
{
    public static IEnumerable<Phone> GetPreconfiguredPhones(
        IEnumerable<PhoneNote> phoneNotes,
        IEnumerable<Counterparty> counterparties)
    {
        Random rnd = new();
        Counterparty[] shuffledList = [.. counterparties.OrderBy(x => rnd.Next())];

        int i = -1;

        List<Phone> phoneList = [];

        foreach (Counterparty counterparty in shuffledList)
        {
            foreach (PhoneNote note in phoneNotes)
            {
                // First
                long number = 380441002001 + ++i;
                Phone phone = new()
                {
                    Id = Guid.NewGuid(),
                    Number = $"+{number}",
                    PhoneNoteId = note.Id,
                    PhoneNote = note,
                    CounterpartyId = counterparty.Id,
                    Counterparty = counterparty,
                };

                phoneList.Add(phone);

            }
        }

        return phoneList;
    }
}
