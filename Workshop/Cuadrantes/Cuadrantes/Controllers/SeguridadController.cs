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
        /// Este metodo es para iniciar sesión, este retorna el Token
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost("Iniciar")]
        public string IniciarSesion(string usuario, string Password)
        {
            //Validación de Usuario que no venga nulo o espacios
            if (string.IsNullOrWhiteSpace(usuario))
                return "Debe ingresar el Usuario";

            //Validación de Password que no venga nulo o espacios
            if (string.IsNullOrWhiteSpace(Password))
                return "Debe ingresar la Contraseña";

            //Se realiza validacion de que el usuario exista en la base de datos
            if (db.InformacionUsuario.Any(x => x.Correo == usuario && x.Clave == Password))
                //Se hace utilización del metodo para obtener el Token
                return GetToken(usuario, Password);
            else
                //Se retorna un mensaje en caso de que el usuario no sea correcto
                return "El usuario o contraseña Incorrectos";
        }

        /// <summary>
        /// Este metodo sirve para generar o obtener el token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetToken(string username, string password)
        {
            //Validación de Usuario que no venga nulo o espacios
            if (string.IsNullOrWhiteSpace(username))
                return "Debe ingresar el Usuario";

            //Validación de Password que no venga nulo o espacios
            if (string.IsNullOrWhiteSpace(password))
                return "Debe ingresar la Contraseña";

            //Se realiza objeto partiendo del molde Common
            var common = new Common();     
            //Se realiza la la captura de los datos del usuario en la base de datos
            var user = db.InformacionUsuario.FirstOrDefault(x => x.Correo == username && x.Clave == password);
            //Se valida que el usuario no sea nulo y se retorna un mensaje
            if (user == null)
                return "Credenciales no validas";
            
            //se establece la entidad del token con un tiempo de expiración fijo de 1 minuto
            var token = new SecurityToken
            {
                DisplayName = string.Concat("Mr ", user.Nombre),
                Expiration = DateTime.Now.AddSeconds(3),
                Username = user.Correo,        
                id = Guid.NewGuid()
            };

            //Se realiza la serealización del objeto a Json o texto
            var tokenString = JsonConvert.SerializeObject(token);
            //Se cifra y se codifica a UTF 8 el resultado obteniendo los bytes de la codificación 
            var tokenBytes = Encoding.UTF8.GetBytes(common.Encrypt(tokenString));
            //Se retorna la codificación a Base64 para su transporte por HTTP
            return Convert.ToBase64String(tokenBytes);
        }


        [HttpPost("Registro")]
        public string Registro([Bind]InformacionUsuario informacionUsuario)
        {
            if (db.InformacionUsuario.Any(x => x.Correo == informacionUsuario.Correo))
                return "Ya existe un usuario registrado con ese correo";

            if (db.InformacionUsuario.Any(x => x.Cedula == informacionUsuario.Cedula))
                return "Ya existe un usuario registrado con ese numero de cedula";

            if (ModelState.IsValid)
            {
                var common = new Common();
                var claveSHA256 = common.GenerateSHA256(informacionUsuario.Clave);
                informacionUsuario.Clave = claveSHA256;


                db.InformacionUsuario.Add(informacionUsuario);
                db.SaveChanges();
                return "Usuario registrado correctamente";
            }

            StringBuilder sb = new StringBuilder();

            ModelState.Values.Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors).ToList()
                .ForEach(x => sb.AppendLine(x.ErrorMessage));

            return $"Datos incorrectos, intentelo nuevamente \n {sb.ToString()}";
        }
    }
}