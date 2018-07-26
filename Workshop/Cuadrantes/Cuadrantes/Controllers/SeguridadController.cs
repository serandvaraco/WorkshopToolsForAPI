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
        public string IniciarSesion(string usuario, string clave)
        {

            InformacionUsuario User1 = new InformacionUsuario();
            if (User1.Nombre == usuario && User1.Cedula == clave)
            {
                GetToken(usuario, clave);
                return "Bienvenido !!!";

            }
            else if (usuario == null && clave == null)
            {

                return "Datos invalidos, ingrese nuevamente los datos";

            }
            else
            {

                return "Por favor Usted debe registrarse para iniciar sesión";
            }
        }

        const string key = "BeX30vkH8iy5ZMEzGG0qmw==";
        private string GetToken(string username, string cedula)
        {
            var common = new Common();
            var cedulaSHA256 = common.GenerateSHA256(cedula);
            var user = db.InformacionUsuario.FirstOrDefault(x => x.Nombre == username && x.Cedula == cedulaSHA256);
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