namespace WebServer.API.Endpoints.PhoneNotes.GetPhoneNotes;

public record GetPhoneNotesResponse(PaginatedResult<PhoneNoteDto> PhoneNotes);

public class GetPhoneNotes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/phone-notes", async (
            [AsParameters] PaginationRequest request,
            ISender sender) =>
        {
            var query = new GetPhoneNotesQuery(request);

            var result = await sender.Send(query);

            var response = result.Adapt<GetPhoneNotesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPhoneNotes")
        .Produces<GetPhoneNotesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get phone notes")
        .WithDescription("Get phone notes");
    }
}
