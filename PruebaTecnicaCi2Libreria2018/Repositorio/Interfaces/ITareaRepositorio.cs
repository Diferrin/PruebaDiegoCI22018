using PruebaTecnicaCi2Libreria2018.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2Libreria2018.Repositorio.Interfaces
{
    public interface ITareaRepositorio
    {
        Task<List<Tarea>> ObtenerListadodeTareas(int intTodas, string strTodasPorEstado);

        Task<Tarea> AgregarTarea(Tarea tareaAInsertar);

        Task<Tarea> ActualizarTarea(Tarea tareaAInsertar);

        Task BorrarTarea(string strTareaId);

        Task<Tarea> ObtenerTareaPorId(Guid guTareaId);
    }
}
