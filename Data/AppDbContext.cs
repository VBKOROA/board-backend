using BoardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

    public DbSet<Post> Posts => Set<Post>();
}