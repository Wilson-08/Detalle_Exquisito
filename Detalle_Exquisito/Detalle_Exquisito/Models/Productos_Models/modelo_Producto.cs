using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Productos_Models
{
    public class modelo_Producto
    {
        [Required,MinLength(3),MaxLength(25)]
        [Display(Name ="Codigo Producto")]
        public string Cod_Producto { get; set; }
        
        [Required]
        [Display(Name = "Fotografia")]
        public HttpPostedFileBase foto { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        [Display(Name = "Nombre Producto")]
        public string nombreProducto { get; set; }

        [Required, MinLength(3), MaxLength(300)]
        [Display(Name = "Descripción del Producto")] 
        public string Descripcion { get; set; }


        [Required,Range(1,100000)]
        [Display(Name = "Precio del producto")]
        [DataType(DataType.Currency)]
        public float precio { get; set; }
        
        [Required]
        [Display(Name = "ID del tipo de producto")]
        public int ID_tipoProd { get; set; }

        [Required]
        [Display(Name = "Identificador de Festividad")]
        public int ID_Festividad { get; set; }
    }
}