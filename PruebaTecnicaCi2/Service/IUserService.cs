using PruebaTecnicaCi2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2.Service
{
    public interface IUsuarioService
    {
        Usuario Authenticate(string username, string password);
        IEnumerable<Usuario> GetAll();
    }

}
