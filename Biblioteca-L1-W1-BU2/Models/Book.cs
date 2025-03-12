using System.ComponentModel.DataAnnotations;

namespace Biblioteca_L1_W1_BU2.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Il titolo è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il titolo non può superare i 50 caratteri")]
        public required string? Title { get; set; }

        [Required(ErrorMessage = "L'autore è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Nome dell'autore non può superare i 50 caratteri")]
        public required string? Author { get; set; }

        [Required(ErrorMessage = "Il genere è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il Genere non può superare i 50 caratteri")]
        public required string? Genre { get; set; }

        public required bool Available { get; set; }

        [Required]
        public required string? CoverUrl { get; set; }
        
        public virtual ICollection<Prestito> Prestiti { get; set; }
    }
}
