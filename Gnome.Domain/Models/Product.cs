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
        
        // Database constraints only - validation handled by FluentValidation
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Slug { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [MaxLength(50)]
        public string NumberOfPlayers { get; set; }

        [MaxLength(50)]
        public string PlayingTime { get; set; }

        [MaxLength(50)]
        public string CommunityAge { get; set; }

        [MaxLength(50)]
        public string Complexity { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public string Awards { get; set; }
        
        public int Stock { get; set; }
        
        public DateTime? CreatedDateTime { get; set; }
        
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
