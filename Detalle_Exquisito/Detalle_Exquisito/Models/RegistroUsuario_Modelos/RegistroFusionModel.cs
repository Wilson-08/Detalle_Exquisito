using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.RegistroUsuario_Modelos
{
    public class RegistroFusionModel
    {
        public int Id_Per { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Carnet de identidad del apoderado")]
        public int CI_Persona { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre apoderado")]
        public string nombre_Persona { get; set; }

        [Required, MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido Paterno")]
        public string apellido_Paterno { get; set; }

        [MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido Materno")]
        public string apellido_Materno { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero de telefono")]
        [Range(3000000, 79999999, ErrorMessage = "Numero de Telefono no valido")]
        public int nro_telefono { get; set; }

        [Required, MinLength(1), MaxLength(1)]
        [Display(Name = "Cliente o Empleado?")]
        public string tipo { get; set; }

        [Required, MinLength(11), MaxLength(70)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico: ")]
        public string correo_Electronico { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Contraseña: ")]
        public string contraseña { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado: ")]
        public string estado { get; set; }

        [Required]
        [Display(Name = "Identificador de Persona")]
        public int ID_Persona { get; set; }

        [Required]
        [Display(Name = "Rol del Usuario: ")]
        [DataType(DataType.PhoneNumber)]
        public int rol_usuario { get; set; }

    }
}