using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using minicore.Interfaces;

namespace minicore.Entities
{
	[Index(nameof(Title))]
    [Index(nameof(CategoryId))]
	[Index(nameof(AuthorId))]
    public class Post : IAuditable
	{
		public string? Id { get; set; }
		public float FinalScore { get; set; }
		public float FleschKinkaidScore { get; set; }
		public string? LanguageToolMatches { get; set; }
		public string? Summary { get; set; }
		public string? Title { get; set; }

		public string? AuthorId { get; set; }
        public virtual User? Author { get; set; }

		public int CategoryId { get; set; }
		public virtual Category? Category { get; set; }

		public virtual ICollection<Tag>? Tags { get; set; }
		public virtual ICollection<User>? LikedBy { get; set; }

		public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

