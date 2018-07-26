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

        CuadranteDataContext db;

        public SeguridadController(CuadranteDataContext cuadranteDataContext)
        {
            db = cuadranteDataContext;
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

            if (usuario == null && clave == null)
            {
                return "Datos invalidos, ingrese nuevamente los datos";
            }
            return "Bienvenido , Yo edite esto ja jajjaja!!!";

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