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
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.HasIndex(p => p.Name).IsUnique();
            entity.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(c => c.Estado)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            entity.Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(c => c.Estado)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();

            entity.Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(c => c.Estado)
                .HasDefaultValue(true);

            entity.HasMany(c => c.Replies)
                .WithOne(c => c.ParentComment)
                .HasForeignKey(c => c.ParentCommentId);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.Id).ValueGeneratedOnAdd();
        });

    }
}
