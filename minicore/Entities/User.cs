using System;
using Microsoft.AspNetCore.Identity;
using minicore.Interfaces;

namespace minicore.Entities
{
    public class User : IdentityUser, IAuditable
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Post>? Posts { get; set; } 
        public virtual ICollection<Post>? LikedPosts { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

