using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace adopmascotas.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pet> Pets { get; set; }
    public DbSet<Adopter> Adopters { get; set; }
    public DbSet<Adoption> Adoptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // solo una mascota podrá ser elegida una vez
        modelBuilder.Entity<Pet>()
            .HasOne(p => p.Adoption)
            .WithOne(a => a.Pet)
            .HasForeignKey<Adoption>(a => a.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        // Adopter -> Adoptions
        modelBuilder.Entity<Adopter>()
            .HasMany(a => a.Adoptions)
            .WithOne(ad => ad.Adopter)
            .HasForeignKey(ad => ad.AdopterId)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Adoption>()
            .HasIndex(a => a.PetId)
            .IsUnique();
    }
}
