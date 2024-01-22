using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Infrastructure.Initializers;
using rivne.booking.Core.Entities;

namespace rivne.booking.Infrastructure.Context;
public class ApiDbContext : IdentityDbContext
{
	public ApiDbContext() : base() { }
	public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
	public DbSet<User> ApiUsers { get; set; }
	public DbSet<IdentityRole> ApiRoles { get; set; }
	public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		//builder.SeedUsers();
		//builder.SeedRoles();
	}
}
