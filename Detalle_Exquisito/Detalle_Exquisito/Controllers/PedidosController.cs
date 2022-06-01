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