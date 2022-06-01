using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Detalle_Exquisito.Models.Pedidos
{
    public class Pedidos_Models
    {
        public int Nropedido { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha del Pedido")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime fecha_Pedido { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(1,99999,ErrorMessage ="Es necesario introducir el numero de su hogar O numero de puerta del lugar que se hospeda.")]
        [Display(Name ="Numero de la casa Ubicada")]
        public int Nro_Entrega { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Latitud de la Ubicacion")]
        public string Ubicacion_Latitud { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Longitud de la Ubicacion")]
        public string Ubicacion_Longitud { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Costo del Envio")]
        public int Costo_Envio { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Costo Total")]
        public float Costo_Total { get; set; }

        [Display(Name = "Cliente ID")]
        public int ID_cliente { get; set; }

        [Display(Name = "Descuento")]
        public int descuento_ID_P { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Costo Total")]
        public List<detalles_pedido> detalle { get; set; }

    }
}