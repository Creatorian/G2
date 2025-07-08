using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gnome.Domain.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Url { get; set; }
        
        public bool IsPrimary { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
} 