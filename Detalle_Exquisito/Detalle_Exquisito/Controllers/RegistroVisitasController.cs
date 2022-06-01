using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Models;
using Detalle_Exquisito.Models.RegistroUsuario_Modelos;
using Detalle_Exquisito.Models.Usuarios_Modelos;

//string email,string pass, int CI,string nombre, string ApePa,string ApeMa,int nro_tel, int Descuento
namespace Detalle_Exquisito.Controllers
{
    public class RegistroVisitasController : Controller
    {
        // GET: RegistroVisitas
        public ActionResult register_new_User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register_new_User(RegistroFusionModel modelo)
        {
            try
            {
                using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                {
                    var Datos_TblUsuario = new Usuario();
                    var Datos_TblPersona = new Persona();
                        
                    Datos_TblPersona.CI_Persona = modelo.CI_Persona;
                    Datos_TblPersona.nombre_Persona = modelo.nombre_Persona.Trim();
                    Datos_TblPersona.apellido_Paterno_cliente = modelo.apellido_Paterno.Trim();
                    Datos_TblPersona.apellido_Materno_cliente = modelo.apellido_Materno.Trim();
                    Datos_TblPersona.nro_telefono = modelo.nro_telefono;
                    Datos_TblPersona.tipo = "C";
                    bd.Persona.Add(Datos_TblPersona);
                    bd.SaveChanges();

                    Datos_TblUsuario.ID_Persona = obtenerID();
                    Datos_TblUsuario.correo_Electronico = modelo.correo_Electronico.Trim();
                    Datos_TblUsuario.contraseña = Encryptar.Codificar(modelo.contraseña);
                    Datos_TblUsuario.estado = "Habilitado";
                    Datos_TblUsuario.rol = 2;
                    bd.Usuario.Add(Datos_TblUsuario);
                    bd.SaveChanges();

                    return RedirectToAction("Login", "Acceso");
                }
            }
            catch (Exception ex)
            {
                return View(modelo);
                throw new Exception(ex.Message);
            }
        }

        public int obtenerID()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            var ope = from op in bd.Persona
                      orderby op.Id_Per descending
                      select op.Id_Per;
            int ID;
            try
            {
                ID = ope.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
    }
}