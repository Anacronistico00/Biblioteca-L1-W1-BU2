﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_L1_W1_BU2.Models
{
    public class Prestito
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime DataPrestito { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? LimiteRestituzione { get; set; } = DateTime.Now.AddDays(10);

        [DataType(DataType.Date)]
        public DateTime? DataRestituzione { get; set; }

        public Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }

        [NotMapped]
        public bool PrestitoScaduto { get; set; }
    }
}
