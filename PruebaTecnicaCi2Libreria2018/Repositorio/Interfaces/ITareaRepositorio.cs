using Libreria.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Repositorio.Interfaces
{
    public interface ITareaRepositorio
    {
        Task<List<Tarea>> ObtenerListadodeTareas(int todas, bool estado);

        Task<Tarea> AgregarTarea(Tarea tarea);

        Task<Tarea> ActualizarTarea(Tarea tarea);

        Task BorrarTarea(string id);

        Task<Tarea> ObtenerTareaPorId(Guid guTareaId);
    }
}
