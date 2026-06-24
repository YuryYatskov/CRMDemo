namespace WebServer.API.Exceptions;

public class PhoneNotFoundException(Guid id) : NotFoundException("Phone", id);

public class PhoneNoteNotFoundException(Guid id) : NotFoundException("Phone note", id);