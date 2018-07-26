using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuadrantes.Model;
using Cuadrantes.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cuadrantes.Controllers
{
    [Route("api/[controller]")]
    public class SeguridadController : Controller
    {
        private readonly CuadranteDataContext db;

        public SeguridadController(CuadranteDataContext context)
        {
            db = context;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        [HttpPost]
        public string IniciarSesion(string usuario, string Password)
        {

            

            if (db.InformacionUsuario.Any(x=> x.Nombre == usuario && x.Clave ==Password))
            {
                return GetToken(usuario, Password);
            }
            else if (usuario == null && Password == null)
            {

                return "Datos invalidos, ingrese nuevamente los datos";

            }
            else
            {

                return "Por favor Usted debe registrarse para iniciar sesión";
            }        
        }

        public SecurityToken ValidateToken()
        {
            var requestToken = HttpContext.Request.Headers["__TOKEN_SECURITY__"];

            if (string.IsNullOrEmpty(requestToken))
                throw new Exception("Token Invalido");

            //Obtiene los bytes desde el base64 generado en el token 
            byte[] TokenBytes = Convert.FromBase64String(requestToken);
            //obtengo la codificación UTF8 del token
            string TokenUTF8Hash = Encoding.UTF8.GetString(TokenBytes);
            //se obtiene el Json de la codificación 
            string tokenJSON = new Common().Decrypt(TokenUTF8Hash, key);

            //se obtiene el TOKEN 
            SecurityToken SecurityToken =
                JsonConvert.DeserializeObject<SecurityToken>(tokenJSON);

            if (SecurityToken == null)
                throw new Exception("Token Invalido");

            if (SecurityToken.Expiration <= DateTime.Now)
                throw new Exception("Token Expirado");

            //se obtiene el token con la información genrada
            return SecurityToken;
        }

        const string key = "BeX30vkH8iy5ZMEzGG0qmw==";
        private string GetToken(string username, string password)
        {
            var common = new Common();
            var passwordSHA256 = common.GenerateSHA256(password);
            var user = db.InformacionUsuario.FirstOrDefault(x => x.Nombre == username && x.Clave == passwordSHA256);
            if (user == null)
                return "Credenciales no validas";
            //Obtener los nombre de los roles autorizados por el usuario
            //var roles = user.UsersInRoles.Where(x => x.User.UserId == user.UserId)
            //            .Select(x => x.Role.Name).ToArray();
            //se establece la entidad del token con un tiempo de expiración fijo de 1 minuto
            var token = new SecurityToken
            {
                DisplayName = string.Concat("Mr ", user.Nombre),
                Expiration = DateTime.Now.AddHours(1),
                Username = user.Nombre,
                //Roles = roles,
                id = Guid.NewGuid()
            };
            #region Comments
            //Se serializa a formato JSON (Javascript Serialization Object Notation) el token
            /*
             *  {
             *      'DisplayName': 'Mr svargas',
             *      'Expiration' : '1246843218798654', // UTC del Fecha y Hora 
             *      'Username': 'svargas'
             *      'Roles': [{'admin', 'sadmin', 'operador'}]
             *  }
             */
            #endregion 
            var tokenString = JsonConvert.SerializeObject(token);
            //Se cifra y se codifica a UTF 8 el resultado obteniendo los bytes de la codificación 
            var tokenBytes = Encoding.UTF8.GetBytes(common.Encrypt(tokenString, key));
            //Se retorna la codificación a Base64 para su transporte por HTTP
            return Convert.ToBase64String(tokenBytes);
        }
        [HttpPost("Registro")]
        public string Registro(InformacionUsuario informacionUsuario)
        {
            if (ModelState.IsValid)
            {
                db.InformacionUsuario.Add(informacionUsuario);
                db.SaveChanges();
                return "Usuario registrado correcto";
            }
            return "Datos incorrectos, intentelo nuevamente";
        }
    }
}