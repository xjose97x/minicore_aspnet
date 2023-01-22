using System;
namespace minicore.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<PostDto> Posts { get; set; }

        public HomeViewModel(IEnumerable<PostDto> posts)
        {
            Posts = posts;
        }
    }
}

