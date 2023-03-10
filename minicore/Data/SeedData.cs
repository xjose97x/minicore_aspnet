using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using minicore.Data;
using minicore.Entities;

namespace minicore.Models
{
	public static class SeedData
	{
		public static async void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
			{
				var roleStore = new RoleStore<IdentityRole>(context);
				if (!context.Roles.Any())
				{
					await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
				}

				if (!context.Users.Any())
				{
					var userStore = new UserStore<User>(context);
                	var passwordHasher = new PasswordHasher<User>();

					var admin = new User
					{
						Id = Guid.NewGuid().ToString(),
						UserName = "admin@admin.com",
						NormalizedUserName = "ADMIN@ADMIN.COM",
						Email = "admin@admin.com",
						NormalizedEmail = "ADMIN@ADMIN.COM",
						EmailConfirmed = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
						FirstName = "Jose",
						LastName = "Escudero"
					};
					var user1 = new User
					{
						Id = Guid.NewGuid().ToString(),
						UserName = "first@user.com",
						NormalizedUserName = "FIRST@USER.COM",
						Email = "first@user.com",
						NormalizedEmail = "FIRST@USER.COM",
						EmailConfirmed = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
						FirstName = "Silvio",
						LastName = "Rodriguez"
					};

					var user2 = new User
					{
						Id = Guid.NewGuid().ToString(),
						UserName = "second@user.com",
						NormalizedUserName = "SECOND@USER.COM",
						Email = "second@user.com",
						NormalizedEmail = "SECOND@USER.COM",
						EmailConfirmed = true,
						CreatedAt = DateTime.Now,
						UpdatedAt = DateTime.Now,
						FirstName = "Pablo",
						LastName = "Milanes"
					};

					admin.PasswordHash = passwordHasher.HashPassword(admin, "123456");
					user1.PasswordHash = passwordHasher.HashPassword(user1, "123456");
					user2.PasswordHash = passwordHasher.HashPassword(user2, "123456");

					await userStore.AddToRoleAsync(admin, "ADMIN");
                    await userStore.CreateAsync(admin);
					await userStore.CreateAsync(user1);
					await userStore.CreateAsync(user2);
				}

				if (!context.Categories.Any())
				{
					await context.AddRangeAsync(
						new Category("Music"),
						new Category("Videogames"),
						new Category("Technology"),
						new Category("Politics"),
						new Category("Religion"),
						new Category("Movies"),
						new Category("Other")
					);
					await context.SaveChangesAsync();
				}

				if (!context.Tags.Any()) {
					await context.AddRangeAsync(
						new Tag("Tag 1"),
						new Tag("Tag 2"),
						new Tag("Tag 3"),
						new Tag("Tag 4"),
						new Tag("Tag 5")
					);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}

