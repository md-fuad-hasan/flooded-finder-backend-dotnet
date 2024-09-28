﻿namespace flooded_finder_backend.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string Role { get; set; } = "User";

        public ICollection<UserArea> UserAreas { get; set; }
    }
}
