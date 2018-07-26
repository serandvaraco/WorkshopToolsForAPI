using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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

    [DataContract]
    public class InformacionUsuario
    {
        [DataMember]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [DataMember]
        [Display(Name = "Fecha de expedición")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.DateTime)]
        public DateTime FechaExpedicion { get; set; }

        [DataMember]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20)]
        public string Nombre { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [DataMember]
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [DataMember]
        [Display(Name = "Fecha de expedición")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength:11)]
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
