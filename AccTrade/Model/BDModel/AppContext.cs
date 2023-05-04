﻿using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public DbSet<Login> Logins { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public AppContext(): base()
    {
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
        modelBuilder.Entity<Roles>().HasKey(key => key.Id);
    }   
}