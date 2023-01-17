using System;
using Microsoft.EntityFrameworkCore;
using minicore.Interfaces;

namespace minicore.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    [Index(nameof(Title), IsUnique = true)]
    public class Tag : IAuditable
	{
		public int Id { get; set; }
		public string? Slug { get; set; }
		public string? Title { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
    }
}

