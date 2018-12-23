using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PruebaTecnicaCi2Libreria2018.Modelos;

namespace PruebaTecnicaCi2Libreria2018.Contextos
{
    /// <summary>
    /// Clase utilizada para representar el contexto de datos de la aplicacion
    /// </summary>
    public class ContextoDeDatos : DbContext
    {
        #region Propiedades

        public virtual DbSet<Tarea> Tareas { get; set; }

        public virtual DbSet<Usuarios> Usuarios { get; set; }

        #endregion       

        #region Constructores

        public ContextoDeDatos(DbContextOptions<ContextoDeDatos> opciones) : base(opciones)
        {

        }

        public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<ContextoDeDatos>
        {
            ContextoDeDatos IDesignTimeDbContextFactory<ContextoDeDatos>.CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ContextoDeDatos>();
                optionsBuilder.UseSqlServer("Server = ABU-OPEDESA\\SQLEXPRESS; Database = TareasCi2; Trusted_Connection = True; MultipleActiveResultSets = true");
                return new ContextoDeDatos(optionsBuilder.Options);
            }
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder constructorDelModelo)
        {
            constructorDelModelo.Entity<Usuarios>().HasMany(u => u.ColTarea)
                                               .WithOne(t => t.ObjUser)
                                               .HasForeignKey(t => t.IntFkUserId)
                                               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
