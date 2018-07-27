using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cuadrantes.Controllers
{
    [Route("ponal/[controller]")]
    public class RequerimientosPonalController : Controller
    {
        /// <summary>
        /// Peticions the specified identificador peticion.
        /// </summary>
        /// <param name="identificador_peticion">The identificador peticion.</param>
        /// <param name="ubicación">The ubicación.</param>
        /// <param name="usuario">The usuario.</param>
        /// <returns>
        ///  MENSAJE { CUADRANTE , TIEMPO DE RESPUESTA}
        /// </returns>
        [HttpPost]
        public string Peticion(string identificador_peticion,
            string ubicación, string usuario)
         => string.Empty;
    }
}