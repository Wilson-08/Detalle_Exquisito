using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Detalle_Exquisito.Models.Usuarios_Modelos;
namespace Detalle_Exquisito.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        public static int RollGoblal = 0;

        [HttpPost]
        public ActionResult Login(string User,string Pass)
        {
            try
            {
                Pass = Encryptar.Codificar(Pass);
                using (Models.BD_Detalle_ExquisitoEntities db = new Models.BD_Detalle_ExquisitoEntities())
                {
                    var oUser = (from d in db.Usuario
                                 where d.correo_Electronico == User && d.contraseña == Pass
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Error de datos, vuelva a intentarlo";
                        return View();
                    }
                    var nameUsuario = (from w in db.Persona
                                       where w.Id_Per == oUser.ID_Persona
                                       select w.nombre_Persona + " " + w.apellido_Paterno_cliente).FirstOrDefault();

                    Detalle_Exquisito.Controllers.HomeController.UsuarioActual= oUser.ID_Persona;
                    Detalle_Exquisito.Controllers.HomeController.RolActual = oUser.rol;

                    Session["Nombre"] = nameUsuario;
                    Session["Rol"] = oUser.rol;
                    RollGoblal = oUser.rol;
                    Session["User"] = oUser;

                }
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            HomeController.RolActual = 0;
            return RedirectToAction("Index","Home");
        }
    }
}