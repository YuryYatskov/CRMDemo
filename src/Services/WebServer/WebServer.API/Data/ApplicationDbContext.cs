namespace WebServer.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Counterparty> Counterparties { get; set; }

    public DbSet<Phone> Phones { get; set; }

    public DbSet<PhoneNote> PhoneNotes { get; set; }

}
