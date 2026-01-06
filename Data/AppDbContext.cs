using BoardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
         .HasOne(c => c.Post)
         .WithMany(p => p.Comments)
         .HasForeignKey(c => c.PostId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}