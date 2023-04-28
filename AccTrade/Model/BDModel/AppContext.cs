using Microsoft.EntityFrameworkCore;
public class AppContext : DbContext
{
    public DbSet<Login> Logins { get; set; }
    //public DbSet<AddAccount> AddAccounts { get; set; }  
    public DbSet<Form> Forms { get; set; }
    public AppContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MEGHT6N;Initial Catalog=TradeAccs;Integrated Security=True; TrustServerCertificate = True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>().HasKey(Key => Key.Id);
        modelBuilder.Entity<Form>().HasAlternateKey(key => key.Id);
    }
}