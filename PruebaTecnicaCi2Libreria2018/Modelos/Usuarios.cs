using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaCi2Libreria2018.Modelos
{
    /// <summary>
    /// Clase utilizada para representar un usuario en el sistema
    /// </summary>
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        /// <summary>
        /// Coleccion de tareas asociadas alusuario
        /// </summary>
        public virtual ICollection<Tarea> ColTarea { get; set; }
    }
}
