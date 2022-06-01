using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Usuarios_Modelos
{
    public class Lista_Usuarios
    {
        public string correo_Electronico { get; set; }
        public string contraseña { get; set; }
        public string estado { get; set; }
        public int ID_Persona { get; set; }
        public int rol { get; set; }

    }
}