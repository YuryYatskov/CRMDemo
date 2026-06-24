namespace WebServer.API.Endpoints.PhoneNotes.GetPhoneNoteById;

public record GetPhoneNoteByIdResponse(PhoneNoteDto Phone);

public class GetPhoneNoteById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/phone-notes/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetPhoneNoteByIdQuery(id));

            var response = result.Adapt<GetPhoneNoteByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPhoneNoteById")
        .Produces<GetPhoneNoteByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get phone note By Id")
        .WithDescription("Get phone note By Id");
    }
}
