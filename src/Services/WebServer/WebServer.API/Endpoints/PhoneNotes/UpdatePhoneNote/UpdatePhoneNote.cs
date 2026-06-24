namespace WebServer.API.Endpoints.PhoneNotes.UpdatePhoneNote;

public record UpdatePhoneNoteRequest(PhoneNoteDto PhoneNote);

public record UpdatePhoneNoteResponse(bool IsSuccess);

public class UpdatePhoneNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/phone-notes", async (UpdatePhoneNoteRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdatePhoneNoteCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdatePhoneNoteResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdatePhoneNote")
        .Produces<UpdatePhoneNoteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update phone note")
        .WithDescription("Update phone note");
    }
}
