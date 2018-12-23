using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2.Model
{
    public class TareaModel
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "La descripción de la tarea excede el número de caracteres permitidos.")]
        public string Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; }
    }
}
