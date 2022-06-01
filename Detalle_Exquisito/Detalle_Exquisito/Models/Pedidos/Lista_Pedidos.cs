using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Pedidos
{
    public class Lista_Pedidos
    {
        public int Nropedido { get; set; }
        public DateTime fecha_Pedido { get; set; }
        public int Nro_Entrega { get; set; }
        public string Ubicacion_Latitud { get; set; }
        public string Ubicacion_Longitud { get; set; }
        public int Costo_Envio { get; set; }
        public double Costo_Total { get; set; }
        public int ID_cliente { get; set; }
        public int descuento_ID_P { get; set; }
        public List<detalles_pedido> detalle {get;set;}
    }
    public class detalles_pedido
    {
        public int id_detalle { get; set; }
        public int cantidad_compra { get; set; }
        public DateTime fecha_entregado { get; set; }
        public string dedicatoria { get; set; }
        public float  costoEnvio { get; set; }
        public float Total_Detalle { get; set; }
        public string cod_prod_dc { get; set; }
        public int Nropedido { get; set; } 
    }
}
