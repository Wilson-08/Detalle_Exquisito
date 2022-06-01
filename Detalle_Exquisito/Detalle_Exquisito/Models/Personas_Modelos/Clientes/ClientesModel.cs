using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Personas_Modelos.Clientes
{
    public class ClientesModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Carnet de identidad del apoderado")]
        public int Id_Persona { get; set; }
    }
}