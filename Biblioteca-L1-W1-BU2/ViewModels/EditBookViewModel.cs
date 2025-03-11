using System.ComponentModel.DataAnnotations;

namespace Biblioteca_L1_W1_BU2.ViewModels
{
    public class EditBookViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string? Title { get; set; }

        [Required]
        [StringLength(50)]
        public required string? Author { get; set; }

        [Required]
        [StringLength(50)]
        public required string? Genre { get; set; }

        [Required]
        public required bool Available { get; set; }

        [Required]
        public required string? CoverUrl { get; set; }
    }
}
