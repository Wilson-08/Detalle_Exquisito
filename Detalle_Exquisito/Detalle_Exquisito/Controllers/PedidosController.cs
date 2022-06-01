using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Filtros;
using Detalle_Exquisito.Models;
using Detalle_Exquisito.Models.Pedidos;
using Detalle_Exquisito.Models.Personas_Modelos;
using Detalle_Exquisito.Models.Productos_Models;
using System.Net.Mail;
using System.Net;

namespace Detalle_Exquisito.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        [Autorizacion_usuario(IdOperacion: 61)]
        public ActionResult ListaPedidos()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            int id = Detalle_Exquisito.Controllers.HomeController.UsuarioActual;
            if (HomeController.RolActual == 2)
            {
                List<Pedido> list_pedido = bd.Pedido.Where(m => m.ID_cliente == id).ToList();
                CargarPersonas();
                return View(list_pedido);
            }
            else
            {
                List<Pedido> list_pedido = bd.Pedido.Take(10).ToList();
                CargarPersonas();
                return View(list_pedido);
            }
        }

        [Autorizacion_usuario(IdOperacion: 65)]
        public ActionResult DetallePedido(int ordenId)
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            List<detalle_Pedido> list_pedido = bd.detalle_Pedido.Where(m => m.Nropedido == ordenId).ToList();
            ViewBag.Lista_Productos = Lista_Productos();
            return PartialView(list_pedido);
        }

        private List<Lista_Productos> Productos_Cargados()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            List<Lista_Productos> list_productos;
            list_productos = (from d in bd.producto
                              select new Lista_Productos
                              {
                                  Cod_Producto = d.Cod_Producto,
                                  nombreProducto = d.nombreProducto,
                                  Descripcion = d.Descripcion,
                                  precio = (float)d.precio,
                                  ID_tipoProd = d.ID_tipoProd,
                                  ID_Festividad_P = d.ID_festividad_P
                              }).ToList();
            return list_productos;
        }

        [Autorizacion_usuario(IdOperacion: 62)]
        public ActionResult AgregarPedido_Cliente()
        {
            ViewBag.Productos_Cargados = Productos_Cargados();
            cargarTipo_De_Productos();
            cargarCliente();
            cargarProductos();
            cargarTipo_De_Productos();
            return View();
        }

        [HttpPost]
        [Autorizacion_usuario(IdOperacion: 66)]
        public ActionResult AgregarPedido_Cliente(Lista_Pedidos model,string horaPedido)
        {
            string valor = "";
            try
            {
                //amenadiel.angeles@gmail.com
                using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                {
                    Pedido traer = new Pedido();
                    model.fecha_Pedido = model.fecha_Pedido.AddHours(Convert.ToDouble(horaPedido.Substring(0, 2))).AddMinutes(Convert.ToDouble(horaPedido.Substring(3, 2)));

                    traer.ID_cliente = HomeController.UsuarioActual;
                    traer.Nro_Entrega = model.Nro_Entrega;
                    traer.Ubicacion_Latitud = model.Ubicacion_Latitud;
                    traer.Ubicacion_Longitud = model.Ubicacion_Longitud;
                    traer.costoEnvio = model.Costo_Envio;
                    traer.fecha_Pedido = Convert.ToDateTime(model.fecha_Pedido);
                    traer.Costo_Total = (float)model.Costo_Total;
                    bd.Pedido.Add(traer);
                    bd.SaveChanges();

                    foreach (var oConcepto in model.detalle)
                    {
                        detalle_Pedido dp = new detalle_Pedido();

                        dp.cantidad_compra = oConcepto.cantidad_compra;
                        dp.fecha_entregado = DateTime.Now;
                        dp.dedicatoria = oConcepto.dedicatoria.Trim().Length>2? oConcepto.dedicatoria.Substring(0,(oConcepto.dedicatoria.Length-2)).Trim():"Ninguna";
                        dp.Total_Detalle = oConcepto.Total_Detalle;
                        dp.cod_prod_dc = oConcepto.cod_prod_dc;
                        dp.Nropedido = traer.Nropedido;
                        bd.detalle_Pedido.Add(dp);
                        valor = valor +
                            "<tr style='border: 1px solid #000;padding: 0px;text-align: center;'>" +
                                "<td style='background-color: #AFAEAB; border: 1px solid #000;padding:0px 0px 0px 15px; text-align: left;'>" + NombreProducto(oConcepto.cod_prod_dc) + "</td>" +
                                "<td style='background-color: #AFAEAB; border: 1px solid #000;padding: 0px;text-align: center;'>" + oConcepto.cantidad_compra + "</td>" +
                                "<td style='background-color: #AFAEAB; border: 1px solid #000;padding: 0px;text-align: center;'>" + oConcepto.Total_Detalle + "Bs</td>" +
                            "</tr>";
                    }
                    bd.SaveChanges();
                    EnviarCorreo(valor, (float)model.Costo_Total);
                }
                return Redirect("/Pedidos/ListaPedidos");
            }
            catch (Exception)
            {

                return View(model);
            }
        }




        [Autorizacion_usuario(IdOperacion: 62)]
        public ActionResult AgregarPedidos()
        {
            cargarCliente();
            cargarProductos();
            cargarTipo_De_Productos();
            return View();
        }

        [HttpPost]
        [Autorizacion_usuario(IdOperacion: 66)]
        public ActionResult AgregarPedidos(Lista_Pedidos model)
        {
            try
            {
                using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                {
                    Pedido traer = new Pedido();
                    if (HomeController.RolActual == 2)
                    {
                        traer.ID_cliente = Detalle_Exquisito.Controllers.HomeController.UsuarioActual;
                    }
                    else
                    {
                        traer.ID_cliente = model.ID_cliente;
                    }
                    traer.Nro_Entrega = model.Nro_Entrega;
                    traer.Ubicacion_Latitud = model.Ubicacion_Latitud;
                    traer.Ubicacion_Longitud = model.Ubicacion_Longitud;
                    traer.costoEnvio = model.Costo_Envio;
                    traer.fecha_Pedido = model.fecha_Pedido;
                    traer.Costo_Total = (float)model.Costo_Total;

                    bd.Pedido.Add(traer);
                    bd.SaveChanges();

                    foreach (var oConcepto in model.detalle)
                    {
                        detalle_Pedido dp = new detalle_Pedido();
                        dp.id_detalle = oConcepto.id_detalle;
                        dp.cantidad_compra = oConcepto.cantidad_compra;
                        dp.fecha_entregado = DateTime.Now;
                        dp.dedicatoria = oConcepto.dedicatoria.Length > 2 ? oConcepto.dedicatoria : "Ninguno";
                        dp.Total_Detalle = oConcepto.Total_Detalle;
                        foreach (var item in Lista_Productos())
                        {
                            if (item.nombreProducto == oConcepto.cod_prod_dc)
                            {
                                oConcepto.cod_prod_dc = item.Cod_Producto;
                            }
                        }
                        dp.cod_prod_dc = oConcepto.cod_prod_dc;
                        dp.Nropedido = traer.Nropedido;
                        bd.detalle_Pedido.Add(dp);
                    }
                    bd.SaveChanges();
                }
                return Redirect("/Pedidos/ListaPedidos");
            }
            catch (Exception)
            {

                return View(model);
            }
        }
        public List<Lista_Productos> Lista_Productos()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            List<Lista_Productos> list_producto;
            list_producto = (from c in bd.producto
                             select new Lista_Productos
                             {
                                 Cod_Producto = c.Cod_Producto,
                                 nombreProducto = c.nombreProducto,
                                 precio = (float)c.precio,
                                 ID_tipoProd = c.ID_tipoProd
                             }).ToList();

            return list_producto;
        }
        public void cargarProductos()
        {
            List<SelectListItem> itemProducto = Lista_Productos().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombreProducto,
                    Value = d.precio.ToString(),
                    Selected = false
                };
            });
            ViewBag.nombres_Productos = itemProducto;
        }
        private void CargarPersonas()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            
            List<Tabla_Personas> listaPersonas;
            listaPersonas = (from c in bd.Persona
                               select new Tabla_Personas
                               {
                                   Id_Per=c.Id_Per,
                                   nombre_Persona = c.nombre_Persona,
                                   apellido_Paterno=c.apellido_Paterno_cliente,
                                   apellido_Materno=c.apellido_Materno_cliente
                               }).ToList();
            ViewBag.PersonaLista = listaPersonas;
        }
        public void cargarTipo_De_Productos()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            List<Lista_Productos> list_T_producto;
            list_T_producto = (from c in bd.Tipo_Producto
                               select new Lista_Productos
                               {
                                   ID_tipoProd = c.ID,
                                   nombreProducto = c.nombre
                               }).ToList();
            List<SelectListItem> itemTipoProductos = list_T_producto.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombreProducto,
                    Value = d.ID_tipoProd.ToString(),
                    Selected = false
                };
            });
            ViewBag.TipoProductos = itemTipoProductos;
        }
        public void cargarCliente()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();

            List<Persona> list_Persona = bd.Persona.Where(m => m.tipo == "C").ToList();

            List<SelectListItem> itemProducto = list_Persona.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_Persona + " " + d.apellido_Paterno_cliente + " /" + d.CI_Persona,
                    Value = d.Id_Per.ToString(),
                    Selected = false
                };
            });
            ViewBag.nombreClientes = itemProducto;
        }
        private void EnviarCorreo(string valor, float Costo_Total)
        {
            string saludo = "";
            if (DateTime.Now.Hour>=6 && DateTime.Now.Hour<12)
            {
                saludo = "Buenos Dias";
            }
            if (DateTime.Now.Hour>=12 && DateTime.Now.Hour<=18)
            {
                saludo = "Buenas Tardes";
            }
            if (DateTime.Now.Hour >18 && DateTime.Now.Hour <23 || DateTime.Now.Hour >=0 && DateTime.Now.Hour <6)
            {
                saludo = "Buenas Noches";
            }
            string Mensaje =
                "<body>" +
                "<h3>"+saludo+" "+ Session["Nombre"] + " su pedido fue Confirmado!</h3>" +
                "<h2 style='text-align:center;'>Lista del Pedido</h2>" +
                "<table style='margin:auto; background-color: #000; border: 1px solid #000;padding: 0px; text-align: center; width: 650px;'>" +
                "<tr style='border: 1px solid #000;padding: 0px;text-align: center; background-color: #E3CB1C;'>" +
                    "<th style='border: 1px solid #000;padding: 0px;text-align: center;'>Producto</th>" +
                    "<th style='border: 1px solid #000;padding: 0px;text-align: center;'>Cantidad</th>" +
                    "<th style='border: 1px solid #000;padding: 0px;text-align: center;'>Total</th>" +
                "</tr>" +
                valor+
                "<tr style='border: 1px solid #000;padding: 0px;text-align: center;'>" +
                    "<td colspan='3' style='background-color: #AFAEAB; border: 1px solid #000; padding:0px 17px 0px 0px; text-align: right;'><span style='font-weight:bold;'>Costo Total: " + Costo_Total+"Bs</span></td>" +
                "</tr>" +
                "</table>" +
                "<p>Muchas gracias por confiar en nosotros, esperamos que los productos sean de su mayor agrado.</p>" +
                "<span>¡Saludos cordiales!</span><br>" +
                "<a href='pranx.com/hacker/' target='1'>Presiona Aqui para compartir tus datos personales..</a>" +
                "</body>";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Port = 25;
            smtpClient.EnableSsl = true;    
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("labola.malo3@gmail.com", "080201080569250300");

            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            var personaEmail= bd.Usuario.Where(m => m.ID_Persona == Detalle_Exquisito.Controllers.HomeController.UsuarioActual).ToList();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("labola.malo3@gmail.com", "Pedidos Detalle Exquisito");
            //mail.To.Add(new MailAddress(personaEmail[0].correo_Electronico));
            mail.To.Add(new MailAddress("labola.malo3@gmail.com"));
            mail.Subject = "Mensaje de gatos";
            mail.IsBodyHtml = true;
            mail.Body = Mensaje;

            smtpClient.Send(mail);
        }
        
        private string NombreProducto(string codigo)
        {
            foreach (var gatos in Productos_Cargados())
            {
                if (gatos.Cod_Producto == codigo)
                {
                    return gatos.nombreProducto;
                }
            }
            return "Miau bebe";
        }
    }
}