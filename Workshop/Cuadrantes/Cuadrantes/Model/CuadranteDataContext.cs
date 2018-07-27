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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
            ErrorMessage = "La contraseña debe tener una longitud de 8 máximo 15 carácteres, debe cumplir con: 1 Mayuscula, 1 Minuscula, 1 Carácter especial, 1 Número ")]
        public string Clave { get; set; }

        [DataMember]
        [Display(Name = "Fecha de expedición")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.DateTime, ErrorMessage ="No es una fecha valida")]
        public DateTime FechaExpedicion { get; set; }

        [DataMember]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 20, ErrorMessage = "La longitud máxima es de 20 carácteres")]
        public string Nombre { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ser una cuenta de correo valida")]
        public string Correo { get; set; }

        [DataMember]
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "No es un número valido")]
        public string Telefono { get; set; }

        [DataMember]
        [Display(Name = "Fecha de expedición")]
        [Required(ErrorMessage = "El campo es requerido")]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 11, ErrorMessage = "El valor máximo permitido es de 11")]
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
