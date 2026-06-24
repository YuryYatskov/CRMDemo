namespace WebServer.API.Endpoints.Phones.GetPhones;

public record GetPhonesQuery(PaginationRequest PaginationRequest)
    : IQuery<GetPhonesResult>;

public record GetPhonesResult(PaginatedResult<PhoneDto> Phones);

public class GetPhonesHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPhonesQuery, GetPhonesResult>
{
    public async Task<GetPhonesResult> Handle(GetPhonesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Phones.AsNoTracking()

            .LongCountAsync(cancellationToken);

        var phones = await dbContext.Phones.AsNoTracking()
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Include(p => p.PhoneNote)
            .Include(c => c.Counterparty)
            .OrderBy(o => o.Number)
            .ToListAsync(cancellationToken);

        var phonesResult = new GetPhonesResult(
            new PaginatedResult<PhoneDto>(
                pageIndex,
                pageSize,
                totalCount,
                phones.ToPhonesDtoList()));

        return phonesResult;
    }
}