using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCi2.Model;
using PruebaTecnicaCi2.Service;
using Libreria.Modelos;
using Libreria.Repositorio.Interfaces;

namespace PruebaTecnicaCi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        #region Constantes

        private readonly ITareaRepositorio _repositorio;

        private IUsuarioService _servicio;
        
        #endregion

        #region Constructores

        public TareasController(ITareaRepositorio tareaRepositorio, IUsuarioService userService)
        {
            _repositorio = tareaRepositorio;
            _servicio = userService;
        }

        #endregion

        [HttpGet("ObtenerTaresa/{todos}/{estado}")]
        public async Task<ActionResult<IEnumerable<Tarea>>> Consultar( int todos, bool estado)
        {
            return await _repositorio.ObtenerListadodeTareas(todos, estado);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> Consultar()
        {
            return await _repositorio.ObtenerListadodeTareas(1, true);
        }

        [HttpPost("crearTarea")]
        public async Task<ActionResult<Tarea>> Crear([FromBody] TareaModel modeloDeTarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _repositorio.AgregarTarea(ObtenerTareaDominio(modeloDeTarea));
                }

                throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost("editarTarea")]
        public async Task<ActionResult<Tarea>> Actualizar([FromBody] TareaModelActualizar TareaModelo, string IdTarea)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var tareaAModificar = await _repositorio.ObtenerTareaPorId(new Guid(IdTarea));
                
                if (tareaAModificar != null && tareaAModificar.Id == new Guid(IdTarea))
                {
                    return await _repositorio.ActualizarTarea(ObtenerTareaDominioParaActualizar(TareaModelo));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpPost("borrarTarea/{id}")]
        public async Task Borrar(string id)
        {
            await _repositorio.BorrarTarea(id);
        }

        private Tarea ObtenerTareaDominio(TareaModel modelo)
        {
            return new Tarea
            {
                Estado = false,
                FechaCreacion = DateTime.Now,
                FechaVencimineto = modelo.FechaVencimiento,
                Id = Guid.NewGuid(),
                UsuarioId = modelo.UsuarioId,
                Descripcion = modelo.Descripcion
            };
        }

        private Tarea ObtenerTareaDominioParaActualizar(TareaModelActualizar modelo)
        {
            return new Tarea
            {
                Estado = modelo.Estado,
                FechaCreacion = modelo.FechaCreacion,
                FechaVencimineto = modelo.FechaVencimiento,
                Id = modelo.GuTareaId,
                UsuarioId = modelo.UsuarioId,
                Descripcion = modelo.Descripcion
            };
        }
    }
}