using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gnome.Domain.Models
{
    public class AdminUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        // Database constraints only - validation handled by FluentValidation
        [MaxLength(100)]
        public string Username { get; set; }
        
        [MaxLength(255)]
        public string Email { get; set; }
        
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
        public DateTime? LastLoginDateTime { get; set; }
        
        public DateTime? LastPasswordChangeDateTime { get; set; }
        
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
} 