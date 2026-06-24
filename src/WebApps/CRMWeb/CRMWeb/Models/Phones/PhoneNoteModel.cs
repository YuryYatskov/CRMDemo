using BuildingBlocks.DTO.Dtos;
using CRMWeb.Helpers.Pagination;

namespace CRMWeb.Models.Phones;

public class PhoneNoteModel : PhoneNoteDto;

// Wrapper classes.
public record GetPhoneNotesResponse(PaginatedResult<PhoneNoteModel> PhoneNotes);
public record GetPhoneNoteByIdResponse(PhoneNoteModel PhoneNote);

public record CreatePhoneNoteResponse(Guid Id);
public record CreatePhoneNoteRequest(PhoneNoteModel PhoneNote);


public record UpdatePhoneNoteResponse(bool IsSuccess);
public record UpdatePhoneNoteRequest(PhoneNoteModel PhoneNote);

public record DeletePhoneNoteResponse(Guid Id);
