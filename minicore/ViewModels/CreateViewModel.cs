using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using minicore.Entities;

namespace minicore.ViewModels
{
	public class CreateViewModel
	{
        [Required]
        public string? Title { get; set; }

        public string? Content { get; set; }
        
        public string CategoryId { get; set; } = string.Empty;

        public SelectList Categories { get; set; }

        public CreateViewModel()
        {
        }

        public CreateViewModel(IEnumerable<Category> categories)
        {
            this.Categories = new SelectList(categories, "Id", "Title");
        }
    }
}

