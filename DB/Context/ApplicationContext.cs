using Microsoft.EntityFrameworkCore;
using NoteApp.DB.Entities;

namespace NoteApp.DB.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Reminder> Reminders => Set<Reminder>();
    public DbSet<Tag> Tags => Set<Tag>();
    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NoteAppDB;Username=postgres;Password=gjcnuhtc5165");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithOne(t => t.Note)
            .HasForeignKey(t => t.NoteTitle)
            .HasPrincipalKey(n => n.Title);

        modelBuilder.Entity<Reminder>()
            .HasMany(r => r.Tags)
            .WithOne(t => t.Reminder)
            .HasForeignKey(t => t.ReminderTitle)
            .HasPrincipalKey(r => r.Title);

        //modelBuilder.Entity<Tag>().HasAlternateKey(t => t.Name);
    }
}