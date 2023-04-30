using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows.Markup;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

public class AppContext : DbContext
{
    public DbSet<Login> Logins { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Category> Categories { get; set; }
    public AppContext(): base()
    {
        //ChangeTracker.AutoDetectChangesEnabled = true;
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MEGHT6N;Initial Catalog=TradeAcc;Integrated Security=True; TrustServerCertificate = True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>().HasKey(Key => Key.Id);
        modelBuilder.Entity<Form>().HasKey(key => key.Id);
        modelBuilder.Entity<Category>().HasKey(key => key.Id);
    }   

}
public partial class ResetIds : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DBCC CHECKIDENT ('YourTableName', RESEED, 0);");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Здесь вы можете определить метод для отмены изменений, сделанных в Up.
    }
}