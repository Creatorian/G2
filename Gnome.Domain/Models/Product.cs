using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gnome.Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Slug { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int Stock { get; set; }
        
        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; }
        
        public DateTime? CreatedDateTime { get; set; }
        
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
