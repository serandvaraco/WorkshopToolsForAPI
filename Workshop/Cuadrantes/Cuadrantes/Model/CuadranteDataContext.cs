using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Cuadrantes.Model
{
    public class CuadranteDataContext : DbContext
    {
         
        public CuadranteDataContext(DbContextOptions<CuadranteDataContext> options)
            : base(options)
        { }

        public DbSet<InformacionPolicial> InformacionPolicial { get; set; }
        public DbSet<InformacionUsuario> InformacionUsuario { get; set; }
        public DbSet<UbicacionAlarmas> UbicacionAlarmas { get; set; }
        public DbSet<UbicacionCuadrante> UbicacionCuadrantes { get; set; }

    }


    public class InformacionPolicial
    {
        public int Id { get; set; }
        public int UbicacionCuadranteId { get; set; }
        public virtual UbicacionCuadrante Cuadrante { get; set; }
    }

    public class InformacionUsuario
    {
        public int Id { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }

    }

    public class UbicacionAlarmas
    {
        public int Id { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int UbicacionCuadranteId { get; set; }

        public virtual UbicacionCuadrante UbicacionCuadrante { get; set; }
    }

    public class UbicacionCuadrante
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string NumeroCuadrante { get; set; }

        public virtual ICollection<UbicacionAlarmas> UbicacionAlarmas { get; set; }

        public virtual ICollection<InformacionPolicial> InformacionPolicial { get; set; }


    }
}
