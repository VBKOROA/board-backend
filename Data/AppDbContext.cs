using BoardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
}