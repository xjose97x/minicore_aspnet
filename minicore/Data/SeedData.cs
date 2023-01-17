using System;
using Microsoft.EntityFrameworkCore;
using minicore.Data;

namespace minicore.Models
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
			{
				if (context.Users.Any())
				{
					return; // DB has already been seeded
				}
			}
		}
	}
}

