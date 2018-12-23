using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaCi2.Model;
using PruebaTecnicaCi2.Utilidades;
using PruebaTecnicaCi2Libreria2018.Contextos;
using PruebaTecnicaCi2Libreria2018.Modelos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCi2.Service
{
    public class UserService : IUserService
    {
        #region Constantes

        private readonly DbContextOptions<ContextoDeDatos> _opciones;

        private readonly AppSettings _appSettings;

        public List<User> _users { get; }

        #endregion

        #region Constructores

        public UserService(DbContextOptions<ContextoDeDatos> opciones, IOptions<AppSettings> appSettings)
        {
            _opciones = opciones;
            _appSettings = appSettings.Value;
            using (var contexto = new ContextoDeDatos(opciones))
            {
                _users = ObtenerUsuarios(contexto.Usuarios.ToList());
            }
        }

        #endregion
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => { x.Password = null; return x; });
        }

        public List<User> ObtenerUsuarios(List<Usuarios> usuarios)
        {
            return usuarios.Select(u => new User
            {
                FirstName = u.FirstName,
                Id = u.Id,
                LastName = u.LastName,
                Token = u.Token,
                Username = u.Username
            }).ToList();
        }
    }
}
