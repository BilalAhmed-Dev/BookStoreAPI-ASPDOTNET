using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookDTO
    {
        public int? BookId { get; set; } // Nullable to allow for creation without an ID

        [Required]
        [StringLength(100)] // Assuming a maximum length for the title
        public string Title { get; set; }

        [Required]
        [StringLength(100)] // Assuming a maximum length for the author
        public string Author { get; set; }

        [Required]
        [StringLength(50)] // Assuming a maximum length for the genre
        public string Genre { get; set; }

        [Required]
        [Range(0, int.MaxValue)] // Assuming price cannot be negative
        public int Price { get; set; }

        [Required]
        public AvailabilityStatus AvailabilityStatus { get; set; } // Enum for availability status

        public int? OriginPrice { get; set; } // Nullable for optional field
        public string? Description { get; set; } // Nullable for optional field
        public string? ImageUrl { get; set; } // Nullable for optional field
        public int? Inventory { get; set; } // Nullable for optional field
    }

    public enum AvailabilityStatus
    {
        IN_STOCK,
        OUT_OF_STOCK
    }
}