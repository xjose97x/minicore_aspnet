using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using minicore.Interfaces;
using minicore.Models;
using Newtonsoft.Json;

namespace minicore.Entities
{
	[Index(nameof(Title))]
    [Index(nameof(CategoryId))]
	[Index(nameof(AuthorId))]
    public class Post : IAuditable
	{
		public string? Id { get; set; }
		public float FleschKinkaidScore { get; set; }
		public string? LanguageToolMatches { get; set; }
		public string? Title { get; set; }
        public string Content { get; set; }

        public string? AuthorId { get; set; }
        public virtual User? Author { get; set; }

		public int CategoryId { get; set; }
		public virtual Category? Category { get; set; }

		public virtual ICollection<Tag>? Tags { get; set; }
		public virtual ICollection<User>? LikedBy { get; set; }

		public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public LanguageToolResult SerializedLanguageToolMatches
        {
            get => JsonConvert.DeserializeObject<LanguageToolResult>(LanguageToolMatches!)!;
        }

        public float FinalScore
        {
            get {
                var grammarScore = (100 - (SerializedLanguageToolMatches.Matches.Count() * 10));
                grammarScore = grammarScore < 0 ? 0 : grammarScore;
                var likesScore = LikedBy!.Count() * 10;
                return ((float)((FleschKinkaidScore * 0.5) + ((float)grammarScore * 0.5) + (float)likesScore));
            }
        }

        public string Summary
        {
            get => Content.Length <= 200 ? Content : Content.Substring(0, 200);
        }


        public Post() { }

        public Post(string title, string content, string authorId, int categoryId, float fleschKinkaidScore, LanguageToolResult languageToolMatches)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Content = content;
            FleschKinkaidScore = fleschKinkaidScore;
            LanguageToolMatches = JsonConvert.SerializeObject(languageToolMatches);
            AuthorId = authorId;
            CategoryId = categoryId;
        }
    }
}

