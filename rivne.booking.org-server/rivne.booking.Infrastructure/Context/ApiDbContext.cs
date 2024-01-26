using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Entities;
using rivne.booking.Core.Entities.Apartments;
 

namespace rivne.booking.Infrastructure.Context;
public class ApiDbContext : IdentityDbContext
{
	public ApiDbContext() : base() { }
	public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
	public DbSet<User> ApiUsers { get; set; }
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
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Apartment>()
			.HasOne(c => c.User)
			.WithMany(r => r.Apartments)
			.HasForeignKey(c => c.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Image>()
			.HasOne(c => c.Apartment)
			.WithMany(r => r.Images)
			.HasForeignKey(c => c.ApartmentId)
			.OnDelete(DeleteBehavior.Cascade);

	}
}
