using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Personas_Modelos
{
    public class PersonasModel
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
        [Display(Name ="Numero de telefono")]
        [Range(3000000,79999999,ErrorMessage = "Numero de Telefono no valido")]
        public int nro_telefono { get; set; }
        
        [Required,MinLength(1),MaxLength(1)]
        [Display(Name ="Cliente o Empleado?")]
        public string tipo { get; set; }

        public int id_cargoEmpleado { get; set; }

    }
}