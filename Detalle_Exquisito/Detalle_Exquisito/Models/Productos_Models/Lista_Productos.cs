using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Detalle_Exquisito.Models;
namespace Detalle_Exquisito.Models.Productos_Models
{
    public class Lista_Productos
    {
        public string Cod_Producto {get;set;}
        public HttpPostedFileBase foto { get; set; }
        public string nombreProducto { get; set; }
        public string Descripcion { get; set; }
        public float precio {get;set;}
        public int ID_tipoProd {get;set;}
        public int ID_Festividad_P { get; set; }
    }
}