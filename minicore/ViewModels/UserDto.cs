using System;
using minicore.Entities;

namespace minicore.ViewModels
{
	public class UserDto
	{
		public string UserId { get; set; }
		public string Name { get; set; }


        public UserDto(User user) : this(user.Id, user.FirstName + " " + user.LastName)
        {

        }

        public UserDto(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}

