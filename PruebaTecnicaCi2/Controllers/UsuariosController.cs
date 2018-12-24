using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCi2.Model;
using PruebaTecnicaCi2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody]Usuario userParam)
        {
            var user = _usuarioService.Authenticate(userParam.Username, userParam.Password);
            if (user == null) return BadRequest(new { message = "El nombre de usuario o contraseña incorrecta." });
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _usuarioService.GetAll();
            return Ok(users);
        }
    }
}
