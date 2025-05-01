using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }

        [Required]
        public long Cedula { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Genero { get; set; } = string.Empty;

        [Required]
        public string? Telefono { get; set; }
    }
}
