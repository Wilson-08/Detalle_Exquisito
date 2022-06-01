using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Detalle_Exquisito.Models;
namespace Detalle_Exquisito.Models.Productos_Models
{
    public class Consulta_product
    {
        BD_Detalle_ExquisitoEntities bd;
        public Consulta_product()
        {
            bd = new BD_Detalle_ExquisitoEntities();
        }
        public List<producto> GetProductos()
        {
            var consultas = from datos in bd.producto
                            select datos;

            return consultas.ToList(); 
        }
    }
}



