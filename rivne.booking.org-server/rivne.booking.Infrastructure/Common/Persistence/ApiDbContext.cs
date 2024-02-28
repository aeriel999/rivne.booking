using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rivne.Booking.Domain.Users;
using Rivne.Booking.Domain.Apartments;
using rivne.booking.Infrastructure.Common.Identity;

namespace rivne.booking.Infrastructure.Common.Persistence;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext() : base() { }
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    public DbSet<AppUser> ApiUsers { get; set; }
    public DbSet<IdentityRole> ApiRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Image> Images { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Apartment>()
            .HasOne(c => c.Street)
            .WithMany(r => r.Apartments)
            .HasForeignKey(c => c.StreetId)
            .OnDelete(DeleteBehavior.ClientNoAction); 

        builder.Entity<Image>()
            .HasOne(c => c.Apartment)
            .WithMany(r => r.Images)
            .HasForeignKey(c => c.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
