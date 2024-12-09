using System.ComponentModel.DataAnnotations;

namespace Project_Coffe.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public string? MusicUrl { get; set; }
    }
}
