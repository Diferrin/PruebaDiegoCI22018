using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCi2Libreria2018.Modelos
{
    /// <summary>
    /// Clase utilizada para representar el modelo de una tarea en la base de datos
    /// </summary>
    public class Tarea
    {
        [Key]
        public Guid GuTareaId { get; set; }

        [StringLength(200)]
        public string StrDescripcion { get; set; }

        public DateTime DatFechaCreacion { get; set; }

        public DateTime DatFechaVencimineto { get; set; }

        public bool BolEstado { get; set; }

        public int IntFkUserId { get; set; }
        public Usuarios ObjUser { get; set; }

        [StringLength(50)]
        public string Objetivo { get; set; }

        [Range(1, 10)]
        public int NumeroActividades { get; set; }
    }
}
