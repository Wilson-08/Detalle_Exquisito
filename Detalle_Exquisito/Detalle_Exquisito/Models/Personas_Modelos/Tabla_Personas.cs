using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Personas_Modelos
{
    public class Tabla_Personas
    {
        public int Id_Per { get; set; }
        public int CI_Persona { get; set; }
        public string nombre_Persona { get; set; }
        public string apellido_Paterno { get; set; }
        public string apellido_Materno { get; set; }
        public int nro_telefono { get; set; }
        public string tipo { get; set; }
        public int id_cargoEmpleado { get; set; }
    }
}