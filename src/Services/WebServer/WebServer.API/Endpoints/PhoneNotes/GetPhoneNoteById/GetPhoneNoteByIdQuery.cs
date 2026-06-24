namespace WebServer.API.Endpoints.PhoneNotes.GetPhoneNoteById;

public record GetPhoneNoteByIdQuery(Guid PhoneNoteId)
    : IQuery<GetPhoneNoteByIdResult>;

public record GetPhoneNoteByIdResult(PhoneNoteDto PhoneNote);

public class GetPhoneNoteByIdHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPhoneNoteByIdQuery, GetPhoneNoteByIdResult>
{
    public async Task<GetPhoneNoteByIdResult> Handle(GetPhoneNoteByIdQuery query, CancellationToken cancellationToken)
    {
        var phoneNote = await dbContext.PhoneNotes.AsNoTracking()
            //.Where(x => x.SeparatorId == SeparatorId.Of(query.SeparatorId))
            .Where(x => x.Id == query.PhoneNoteId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new PhoneNoteNotFoundException(query.PhoneNoteId);

        return new GetPhoneNoteByIdResult(phoneNote.ToPhoneNoteDto());
    }
}