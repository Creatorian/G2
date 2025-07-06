using System;

namespace Gnome.Domain.DTOs
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
    }
} 