//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Detalle_Exquisito.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Descuento_Cliente
    {
        public int id_DesCl { get; set; }
        public System.DateTime mes_descuento { get; set; }
        public int id_desc { get; set; }
        public int Id_Per { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Descuento Descuento { get; set; }
    }
}
