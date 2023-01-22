using System;
using minicore.Entities;

namespace minicore.ViewModels
{
	public class PostDto
	{
		public string Id { get; set; }
		public string Category { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public float FleschKinkaidScore { get; set; }
		public float FinalScore { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public UserDto Author { get; set; }
		public DateTime CreatedAt { get; set; }

        public PostDto(Post post)
        {
			this.Id = post.Id!;
			this.Category = post.Category!.Title!;
			this.Tags = new List<string>();
			if (post.Tags != null && post.Tags.Any())
			{
				this.Tags = post.Tags.Select(t => t.Title!);
			}
			this.FleschKinkaidScore = post.FleschKinkaidScore;
			this.FinalScore = post.FinalScore;
			this.Title = post.Title!;
			this.Summary = post.Summary!;
			this.Author = new UserDto(post.Author!);
			this.CreatedAt = post.CreatedAt;
        }
    }
}

