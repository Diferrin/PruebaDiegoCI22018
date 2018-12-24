using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libreria.Modelos
{
    /// <summary>
    /// Clase utilizada para representar un usuario en el sistema
    /// </summary>
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Coleccion de tareas asociadas alusuario
        /// </summary>
        public virtual ICollection<Tarea> ColTarea { get; set; }
    }
}
