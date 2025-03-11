using System.ComponentModel.DataAnnotations;

namespace Biblioteca_L1_W1_BU2.ViewModels
{
    public class BookDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public bool Available { get; set; }

        public string? CoverUrl { get; set; }
    }
}
