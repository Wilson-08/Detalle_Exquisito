using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Filtros;
using Detalle_Exquisito.Models;
using Detalle_Exquisito.Models.Productos_Models;

namespace Detalle_Exquisito.Controllers
{
    public class ProductosController : Controller
    {
        Consulta_product traerPro;
        public ProductosController()
        {
            this.traerPro = new Consulta_product();
        }
        // GET: Productos
        [HttpGet]
        [Autorizacion_usuario(IdOperacion: 45)]
        public ActionResult _ViewParcialProductos()
        {
            List<producto> producto = this.traerPro.GetProductos();
            return PartialView(producto);
        }

        [HttpGet]
        //[Autorizacion_usuario(IdOperacion: 45)]
        public ActionResult Productos_Lista()
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
            cargarTipo_De_Productos();
            return View(list_productos);
        }

        public ActionResult AgregarProducto()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto(modelo_Producto prod)
        {
            byte[] fotoWeb = new byte[prod.foto.ContentLength];
            prod.foto.InputStream.Read(fotoWeb, 0, prod.foto.ContentLength);
            string Base64String = Convert.ToBase64String(fotoWeb);
            byte[] newFoto = Convert.FromBase64String(Base64String);

            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var Datos_Tabla = new producto();

                Datos_Tabla.Cod_Producto = prod.Cod_Producto;
                Datos_Tabla.foto = newFoto;
                Datos_Tabla.nombreProducto = prod.nombreProducto;
                Datos_Tabla.Descripcion = prod.Descripcion;
                Datos_Tabla.precio = prod.precio;
                Datos_Tabla.ID_tipoProd = prod.ID_tipoProd;
                Datos_Tabla.ID_festividad_P = prod.ID_Festividad;

                bd.producto.Add(Datos_Tabla);
                bd.SaveChanges();
                return Redirect("/Productos/Productos_Lista");
            }
        } 

        public ActionResult getImagen(string id)
        {
            BD_Detalle_ExquisitoEntities db = new BD_Detalle_ExquisitoEntities();
            producto producto = db.producto.Find(id);
            byte[] byteImage = producto.foto;

            MemoryStream MS = new MemoryStream(byteImage);
            Image image = Image.FromStream(MS);
            MS = new MemoryStream();
            image.Save(MS, ImageFormat.Jpeg);
            MS.Position = 0;

            return File(MS, "image/jpg");
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
        public ActionResult EditarProducto(string Cod_Producto)
        {
            modelo_Producto modelo = new modelo_Producto();
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.producto.Find(Cod_Producto);
                modelo.Cod_Producto = atributos_Tabla.Cod_Producto;
                modelo.nombreProducto = atributos_Tabla.nombreProducto;
                modelo.Descripcion = atributos_Tabla.Descripcion;
                modelo.precio = (float)atributos_Tabla.precio;
                modelo.ID_tipoProd = atributos_Tabla.ID_tipoProd;
                modelo.ID_Festividad = atributos_Tabla.ID_festividad_P;
            }
            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProducto(modelo_Producto prod)
        {
            byte[] fotoWeb = new byte[prod.foto.ContentLength];
            prod.foto.InputStream.Read(fotoWeb, 0, prod.foto.ContentLength);
            string Base64String = Convert.ToBase64String(fotoWeb);
            byte[] newFoto = Convert.FromBase64String(Base64String);

            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = bd.producto.Find(prod.Cod_Producto);
                        Datos_Tabla.Cod_Producto = prod.Cod_Producto;
                        Datos_Tabla.foto = newFoto;
                        Datos_Tabla.nombreProducto = prod.nombreProducto;
                        Datos_Tabla.Descripcion = prod.Descripcion;
                        Datos_Tabla.precio = prod.precio;
                        Datos_Tabla.ID_tipoProd = prod.ID_tipoProd;
                        Datos_Tabla.ID_festividad_P = prod.ID_Festividad;

                        List<producto> lista_Prod = traerPro.GetProductos();
                        /*for (int i = 0; i < lista_Prod.Count; i++)
                        {
                            if (Datos_Tabla.Cod_Producto == lista_Prod[i].Cod_Producto)
                            {
                                HomeController.AlertaGlobal = 2;
                                i = lista_Prod.Count;s
                                return Redirect("/Usuarios/Editar_usuarios?id=" + modelo.correo_Electronico);
                            }
                        }*/
                        bd.Entry(Datos_Tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                        return Redirect("/Productos/Productos_Lista");
                    }
                }
                return View(prod);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[Autorizacion_usuario(IdOperacion: 15)]
        [HttpGet]
        public ActionResult Eliminar_Producto(string Cod_Producto)
        {
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.producto.Find(Cod_Producto);
                if (atributos_Tabla != null)
                {
                    //bd.producto.Remove(atributos_Tabla);
                    //bd.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return Redirect("/Productos/Productos_Lista");
        }

    }
}