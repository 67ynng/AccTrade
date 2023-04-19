using Microsoft.EntityFrameworkCore;
public class AppContext : DbContext
{
    public DbSet<Login> Logins { get; set; }
    public DbSet<AddAccount> AddAccounts { get; set; }
    public AppContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MEGHT6N;Initial Catalog=TradeAcc;Integrated Security=True; TrustServerCertificate = True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>().HasKey(Key => Key.Id);
        modelBuilder.Entity<AddAccount>().HasKey(key => key.Id);
    }
}