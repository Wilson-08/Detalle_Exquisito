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
    
    public partial class detalle_Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public detalle_Pedido()
        {
            this.Detalle_Decoracion_Extras = new HashSet<Detalle_Decoracion_Extras>();
            this.Detalle_Productos_Extras = new HashSet<Detalle_Productos_Extras>();
        }
    
        public int id_detalle { get; set; }
        public int cantidad_compra { get; set; }
        public System.DateTime fecha_entregado { get; set; }
        public string dedicatoria { get; set; }
        public double Total_Detalle { get; set; }
        public string cod_prod_dc { get; set; }
        public int Nropedido { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Decoracion_Extras> Detalle_Decoracion_Extras { get; set; }
        public virtual producto producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_Productos_Extras> Detalle_Productos_Extras { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
