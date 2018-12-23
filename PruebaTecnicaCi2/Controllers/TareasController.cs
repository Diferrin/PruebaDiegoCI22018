using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCi2.Model;
using PruebaTecnicaCi2.Service;
using PruebaTecnicaCi2Libreria2018.Modelos;
using PruebaTecnicaCi2Libreria2018.Repositorio.Interfaces;
using PruebaTecnicaCi2Libreria2018.Utilidades;

namespace PruebaTecnicaCi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        #region Constantes

        private readonly ITareaRepositorio _ITareaRepositorio;

        private IUserService _userService;
        
        #endregion

        #region Constructores

        public TareasController(ITareaRepositorio tareaRepositorio, IUserService userService)
        {
            _ITareaRepositorio = tareaRepositorio;
            _userService = userService;
        }

        #endregion

        /// <summary>
        /// Metodo que lista las tareas registradas de acuerdo a los criterios deseados
        /// </summary>
        /// <param name="intTodas">ID del usuario (autor); 0 en caso de listar las tareas de todos los usuarios</param>
        /// <param name="strTodasPorEstado">tipo de filtro para estado; 1 terminada, 2 NO terminada, default todas las tareas</param>
        /// <returns>Listado de tareas con los filtros deseados</returns>
        [HttpGet("{intTodas}/{strTodasPorEstado}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> Consultar([FromHeader] int intTodas, string strTodasPorEstado)
        {
            return await _ITareaRepositorio.ObtenerListadodeTareas(intTodas, strTodasPorEstado);
        }

        [HttpPost("{modeloDeTarea}")]
        public async Task<ActionResult<Tarea>> Crear([FromBody] TareaModel modeloDeTarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _ITareaRepositorio.AgregarTarea(ObtenerTareaDominio(modeloDeTarea));
                }

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("{strTareaId}")]
        public async Task<ActionResult<Tarea>> Actualizar([FromBody] TareaModelActualizar modeloDeTarea, string strTareaId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var tareaAModificar = await _ITareaRepositorio.ObtenerTareaPorId(new Guid(strTareaId));
                
                if (tareaAModificar != null && tareaAModificar.GuTareaId == new Guid(strTareaId))
                {
                    return await _ITareaRepositorio.ActualizarTarea(ObtenerTareaDominioParaActualizar(modeloDeTarea));
                }
                else
                {
                    return BadRequest(new { message = "Datos incorrectos." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{strTareaId}")]
        public async Task Borrar(string strTareaId)
        {
            await _ITareaRepositorio.BorrarTarea(strTareaId);
        }

        private Tarea ObtenerTareaDominio(TareaModel modelo)
        {
            return new Tarea
            {
                BolEstado = Constantes.EstadoTareaNoTerminada,
                DatFechaCreacion = DateTime.Now,
                DatFechaVencimineto = modelo.FechaVencimiento,
                GuTareaId = Guid.NewGuid(),
                IntFkUserId = modelo.UsuarioId,
                StrDescripcion = modelo.Descripcion
            };
        }

        private Tarea ObtenerTareaDominioParaActualizar(TareaModelActualizar modelo)
        {
            return new Tarea
            {
                BolEstado = modelo.BolEstado,
                DatFechaCreacion = modelo.DatFechaCreacion,
                DatFechaVencimineto = modelo.FechaVencimiento,
                GuTareaId = modelo.GuTareaId,
                IntFkUserId = modelo.UsuarioId,
                StrDescripcion = modelo.Descripcion
            };
        }
    }
}