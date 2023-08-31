using System.ComponentModel.DataAnnotations;

namespace Labb1.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
