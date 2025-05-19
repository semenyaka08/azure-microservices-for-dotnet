using Microsoft.EntityFrameworkCore;
using Wpm.Management.API.Models;

namespace Wpm.Management.API;

public class ManagementDbContext(DbContextOptions<ManagementDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets { get; set; }

    public DbSet<Breed> Breeds { get; set; }
}