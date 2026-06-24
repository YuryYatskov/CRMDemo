namespace WebServer.API.Models;

public class PhoneNote
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Phone>? Phones { get; set; }
}
