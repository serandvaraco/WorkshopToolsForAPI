using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cuadrantes.Controllers
{
    [Route("api/[controller]")]
    public class SeguridadController : Controller
    {
        [HttpPost]
        public string IniciarSesion(string usuario, string clave)
        {
            if (usuario == null && clave == null)
            {
                return "Datos invalidos";
            }
            return "Bienvenido";
        }
        [HttpPost]
        public string Registro(string cedula, DateTime fechaExpedicion,
            string telefono, string correo)
        {
            return string.Empty;
        }
    }
}