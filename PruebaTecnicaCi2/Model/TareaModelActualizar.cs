using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2.Model
{
    public class TareaModelActualizar : TareaModel
    {
        public bool BolEstado { get; set; }

        public DateTime DatFechaCreacion { get; set; }

        public Guid GuTareaId { get; set; }        
    }
}
