using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data;

public class LibraryAppContext : IdentityDbContext<IdentityUser>
{
    public LibraryAppContext(DbContextOptions<LibraryAppContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}