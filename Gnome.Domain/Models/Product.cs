using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<Variant> Variants { get; set; } = new List<Variant>();
    }
}
