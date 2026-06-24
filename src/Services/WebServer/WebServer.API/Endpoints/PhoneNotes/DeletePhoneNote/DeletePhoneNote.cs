namespace WebServer.API.Endpoints.PhoneNotes.DeletePhoneNote;

public record DeletePhoneNoteResponse(bool IsSuccess);

public class DeletePhoneNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/phone-notes/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeletePhoneNoteCommand(Id));

            var response = result.Adapt<DeletePhoneNoteResponse>();

            return Results.Ok(response);
        })
        .WithName("DeletePhoneNote")
        .Produces<DeletePhoneNoteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete phone note")
        .WithDescription("Delete phone note");
    }
}
