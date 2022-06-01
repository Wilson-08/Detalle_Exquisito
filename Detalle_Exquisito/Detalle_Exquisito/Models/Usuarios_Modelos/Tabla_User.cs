using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Usuarios_Modelos
{
    public class Tabla_User
    {//para precios: range(desde que valor, maximo valor)
        [Required,MinLength(11),MaxLength(70)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Correo Electronico: ")]
        public string correo_Electronico { get; set; }
        
        [Required,MinLength(8),MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Contraseña: ")]
        public string contraseña { get; set; }
        
        [StringLength(20)]
        [Display(Name = "Estado: ")]
        public string estado { get; set; }

        [Required]
        [Display(Name ="Identificador de Persona")]
        public int ID_Persona { get; set; }

        [Required]
        [Display(Name = "Rol del Usuario: ")]
        [DataType(DataType.PhoneNumber)]
        public int rol_usuario { get; set; }
    }
}