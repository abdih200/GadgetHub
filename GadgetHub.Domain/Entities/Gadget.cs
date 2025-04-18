using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Domain.Entities
{
    public class Gadget
    {
        public int GadgetId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Category { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}

