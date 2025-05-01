using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "BIGINT")]
        public long Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        [Required]
        [StringLength(20)]
        public string Genero { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [Column(TypeName = "BIT")]
        public bool Eliminado { get; set; } = false; // Valor por defecto false
    }
}