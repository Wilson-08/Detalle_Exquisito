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
    
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.Detalle_producto = new HashSet<Detalle_producto>();
            this.detalle_Pedido = new HashSet<detalle_Pedido>();
        }
    
        public string Cod_Producto { get; set; }
        public byte[] foto { get; set; }
        public string nombreProducto { get; set; }
        public string Descripcion { get; set; }
        public double precio { get; set; }
        public int ID_tipoProd { get; set; }
        public int ID_festividad_P { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_producto> Detalle_producto { get; set; }
        public virtual Festividad Festividad { get; set; }
        public virtual Tipo_Producto Tipo_Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_Pedido> detalle_Pedido { get; set; }
    }
}