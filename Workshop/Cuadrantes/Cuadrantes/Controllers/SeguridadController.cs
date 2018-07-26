using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cuadrantes.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cuadrantes.Controllers
{
    [Route("api/[controller]")]
    public class SeguridadController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        [HttpPost]
        public string IniciarSesion(string usuario, string clave)
        {

            InformacionUsuario User = new InformacionUsuario();
            if (User.Nombre == usuario && User.Cedula == clave)
            {
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
        [HttpPost]
        public string Registro(string cedula, DateTime fechaExpedicion,
            string telefono, string correo)
        {
            return "Usuario registrado correcto";
        }
    }
}