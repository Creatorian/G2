using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gnome.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Token { get; set; }
        
        [Required]
        public DateTime ExpiresAt { get; set; }
        
        [Required]
        public bool IsRevoked { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? RevokedAt { get; set; }
        
        [Required]
        public int AdminUserId { get; set; }
        
        public AdminUser AdminUser { get; set; }
    }
} 