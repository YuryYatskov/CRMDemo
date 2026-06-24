using CRMWeb.Models.Phones;
using Refit;

namespace CRMWeb.Services.PhoneNotes;

public interface IPhoneNoteService
{
    [Get("/phone-notes?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<IApiResponse<GetPhoneNotesResponse>> GetPhoneNotes(int? pageIndex = 0, int? pageSize = 10);

    [Get("/phone-notes/{id}")]
    Task<IApiResponse<GetPhoneNoteByIdResponse>> GetPhoneNote(Guid id);

    [Post("/phone-notes")]
    Task<IApiResponse<CreatePhoneNoteResponse>> CreatePhoneNote(CreatePhoneNoteRequest request);

    [Put("/phone-notes")]
    Task<IApiResponse<UpdatePhoneNoteResponse>> UpdatePhoneNote(UpdatePhoneNoteRequest request);

    [Delete("/phone-notes/{id}")]
    Task<IApiResponse<DeletePhoneNoteResponse>> DeletePhoneNote(Guid id);
}