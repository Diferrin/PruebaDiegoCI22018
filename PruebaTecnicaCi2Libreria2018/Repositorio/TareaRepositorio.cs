using Microsoft.EntityFrameworkCore;
using Libreria.Contextos;
using Libreria.Modelos;
using Libreria.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Repositorio
{
    public class TareaRepositorio : ITareaRepositorio
    {
        #region Constantes

        private readonly DbContextOptions<ContextoDeDatos> _opciones;

        #endregion

        #region Constructores

        /// <summary>
        /// Crea el repositorio para tareas con el contexto de datos pasado como parametro
        /// </summary>
        /// <param name="opciones"></param>
        public TareaRepositorio(DbContextOptions<ContextoDeDatos> opciones)
        {
            _opciones = opciones;
        }

        #endregion

        public async Task<List<Tarea>> ObtenerListadodeTareas(int todos, bool estado)
        {
            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    IQueryable<Tarea> tareas = contexto.Tareas;

                    if (todos != 0)
                    {
                        tareas.Where(t => t.UsuarioId == todos);
                    }

                    if (estado)
                    {
                        tareas.Where(t => t.Estado == true);
                    }
                    else if (!estado)
                    {
                        tareas.Where(t => t.Estado == false);
                    }

                    tareas.OrderBy(t => t.FechaVencimineto);
                    return await tareas.AsNoTracking().ToListAsync();
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }

        public async Task<Tarea> AgregarTarea(Tarea tareaAInsertar)
        {
            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    try
                    {
                        contexto.Add(tareaAInsertar);
                        await contexto.SaveChangesAsync();
                        return tareaAInsertar;
                    }
                    catch (DbUpdateConcurrencyException exepcion)
                    {
                        throw exepcion;
                    }
                    catch (DbUpdateException errorAlGuardar)
                    {
                        throw errorAlGuardar;
                    }
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }

        public async Task<Tarea> ActualizarTarea(Tarea objTareaAModificar)
        {
            if (objTareaAModificar == null || objTareaAModificar.Id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    try
                    {
                        contexto.Update(objTareaAModificar);
                        await contexto.SaveChangesAsync();
                        return objTareaAModificar;
                    }
                    catch (DbUpdateConcurrencyException errorDeConcurrencia)
                    {
                        throw errorDeConcurrencia;
                    }
                    catch (DbUpdateException errorAlGuardar)
                    {
                        throw errorAlGuardar;
                    }
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }

        public async Task BorrarTarea(string strTareaId)
        {
            if (string.IsNullOrWhiteSpace(strTareaId))
            {
                throw new ArgumentNullException();
            }

            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    try
                    {
                        var objTareaABorrar = await contexto.Tareas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == new Guid(strTareaId));

                        if (objTareaABorrar != null)
                        {
                            contexto.Remove(objTareaABorrar);
                            await contexto.SaveChangesAsync();
                        }
                    }
                    catch (DbUpdateConcurrencyException errorDeConcurrencia)
                    {
                        throw errorDeConcurrencia;
                    }
                    catch (DbUpdateException errorAlGuardar)
                    {
                        throw errorAlGuardar;
                    }
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }

        public async Task<Tarea> ObtenerTareaPorId(Guid guTareaId)
        {
            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    IQueryable<Tarea> tareas = contexto.Tareas;                    
                    return await tareas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == guTareaId);
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }
    }
}
