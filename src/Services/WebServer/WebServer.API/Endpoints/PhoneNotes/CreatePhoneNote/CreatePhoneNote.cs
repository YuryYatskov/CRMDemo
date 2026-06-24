namespace WebServer.API.Endpoints.PhoneNotes.CreatePhoneNote;

public record CreatePhoneNoteRequest(PhoneNoteDto PhoneNote);

public record CreatePhoneNoteResponse(Guid Id);

public class CreatePhoneNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/phone-notes", async (CreatePhoneNoteRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreatePhoneNoteCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreatePhoneNoteResponse>();

            return Results.Created($"/phone-notes/{response.Id}", response);
        })
       .WithName("CreatePhoneNote")
       .Produces<CreatePhoneNoteResponse>(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Create phone")
       .WithDescription("Create phone");
    }
}
