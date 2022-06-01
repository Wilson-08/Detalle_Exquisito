using Detalle_Exquisito.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detalle_Exquisito.Controllers
{
    public class HomeController : Controller
    {
        //[Autorizacion_usuario(IdOperacion: 1)]
        public ActionResult Index()
        {
            return View();
        }
        public static int AlertaGlobal=0;
        public static int UsuarioActual = 0;
        public static int RolActual = 0; 

        //[Autorizacion_usuario(IdOperacion: 2)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //[Autorizacion_usuario(IdOperacion: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}