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
    
    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.detalle_Pedido = new HashSet<detalle_Pedido>();
            this.Pago = new HashSet<Pago>();
        }
    
        public int Nropedido { get; set; }
        public System.DateTime fecha_Pedido { get; set; }
        public double costoEnvio { get; set; }
        public Nullable<int> Nro_Entrega { get; set; }
        public string Ubicacion_Latitud { get; set; }
        public string Ubicacion_Longitud { get; set; }
        public double Costo_Total { get; set; }
        public int ID_cliente { get; set; }
        public Nullable<int> descuento_P { get; set; }
    
        public virtual Descuento Descuento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_Pedido> detalle_Pedido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual Persona Persona { get; set; }
    }
}