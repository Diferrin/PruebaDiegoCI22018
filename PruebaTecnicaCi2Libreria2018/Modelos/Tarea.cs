using System;
using System.ComponentModel.DataAnnotations;

namespace Libreria.Modelos
{
    /// <summary>
    /// Clase utilizada para representar el modelo de una tarea en la base de datos
    /// </summary>
    public class Tarea
    {
        [Key]
        public Guid Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaVencimineto { get; set; }

        public bool Estado { get; set; }

        public int UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }

        [StringLength(100)]
        public string Objetivo { get; set; }

        [Range(1, 10)]
        public int NumeroActividades { get; set; }
    }
}
