using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Domain.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // Database constraints only - validation handled by FluentValidation
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Slug { get; set; }
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
