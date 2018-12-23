using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCi2Libreria2018.Contextos;
using PruebaTecnicaCi2Libreria2018.Modelos;
using PruebaTecnicaCi2Libreria2018.Repositorio.Interfaces;
using PruebaTecnicaCi2Libreria2018.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2Libreria2018.Repositorio
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

        public async Task<List<Tarea>> ObtenerListadodeTareas(int intTodas, string strTodasPorEstado)
        {
            try
            {
                using (var contexto = new ContextoDeDatos(_opciones))
                {
                    IQueryable<Tarea> tareas = contexto.Tareas;

                    if (intTodas != 0)
                    {
                        tareas.Where(t => t.IntFkUserId == intTodas);
                    }

                    if (strTodasPorEstado == "1")
                    {
                        tareas.Where(t => t.BolEstado == Constantes.EstadoTareaTerminada);
                    }
                    else if (strTodasPorEstado == "2")
                    {
                        tareas.Where(t => t.BolEstado == Constantes.EstadoTareaNoTerminada);
                    }

                    tareas.OrderBy(t => t.DatFechaVencimineto);
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

        public async Task<Tarea> ActualizarTarea(Tarea objTareaAModificar)
        {
            if (objTareaAModificar == null || objTareaAModificar.GuTareaId == Guid.Empty)
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
                        var objTareaABorrar = await contexto.Tareas.AsNoTracking().FirstOrDefaultAsync(t => t.GuTareaId == new Guid(strTareaId));

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
                    return await tareas.AsNoTracking().FirstOrDefaultAsync(t => t.GuTareaId == guTareaId);
                }
            }
            catch (SqlException errorDeConexion)
            {
                throw errorDeConexion;
            }
        }
    }
}
