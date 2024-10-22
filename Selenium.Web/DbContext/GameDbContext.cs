using Microsoft.EntityFrameworkCore;
using Selenium.Web.Models;

namespace Selenium.Web.Context;


public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Pais> Paises { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Game>()
        .HasKey(g => g.Id);
        modelBuilder.Entity<Game>()
            .Property(g => g.Id)
            .ValueGeneratedOnAdd();


        modelBuilder.Entity<Person>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Person>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Pais>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Pais>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();


        modelBuilder.Entity<Post>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Post>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // modelBuilder.Entity<Game>()
        //     .HasMany(g => g.Posts)
        //     .WithOne(p => p.Game)
        //     .HasForeignKey(p => p.IdGame);


        modelBuilder.Entity<Person>()
            .HasMany(p => p.Posts)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.IdPerson);


        modelBuilder.Entity<Pais>()
            .HasMany(p => p.People)
            .WithOne(p => p.Pais)
            .HasForeignKey(p => p.PaisId);
    }
}
