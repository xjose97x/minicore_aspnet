using System;
using System.Web;
using Microsoft.EntityFrameworkCore;
using minicore.Interfaces;

namespace minicore.Entities
{
	[Index(nameof(Slug), IsUnique = true)]
    [Index(nameof(Title), IsUnique = true)]
    public class Category : IAuditable
	{
        public Category()
        {
        }

        public Category(string title)
        {
            Title = title;
            Slug = HttpUtility.UrlEncode(title);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
		public string? Slug { get; set; }
		public string? Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }

        
    }
}

