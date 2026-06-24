namespace WebServer.API.Endpoints.PhoneNotes.GetPhoneNotes;

public record GetPhoneNotesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetPhoneNotesResult>;

public record GetPhoneNotesResult(PaginatedResult<PhoneNoteDto> PhoneNotes);

public class GetPhoneNotesHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPhoneNotesQuery, GetPhoneNotesResult>
{
    public async Task<GetPhoneNotesResult> Handle(GetPhoneNotesQuery query, CancellationToken cancellationToken)
    {
        // get orders with pagination
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.PhoneNotes.AsNoTracking()
            .LongCountAsync(cancellationToken);

        var phoneNotes = await dbContext.PhoneNotes.AsNoTracking()
            .OrderBy(o => o.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var phoneNotesResult = new GetPhoneNotesResult(
            new PaginatedResult<PhoneNoteDto>(
                pageIndex,
                pageSize,
                totalCount,
                phoneNotes.ToPhoneNotesDtoList()));

        return phoneNotesResult;
    }
}
