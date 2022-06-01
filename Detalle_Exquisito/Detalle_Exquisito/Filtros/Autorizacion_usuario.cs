using Detalle_Exquisito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Controllers;
namespace Detalle_Exquisito.Filtros
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Autorizacion_usuario : AuthorizeAttribute
    {
        private Usuario oUsuario;
        private BD_Detalle_ExquisitoEntities db = new BD_Detalle_ExquisitoEntities();
        private int idOperacion;

        public Autorizacion_usuario(int IdOperacion = 0)//Constructor
        {
            this.idOperacion = IdOperacion;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nomOperacion = "";
            String nomModulo = "";
            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                var ultOperacion = from m in db.Operaciones_Menu
                                   where m.rol_seleccion==oUsuario.rol
                                        && m.id_operaciones==idOperacion
                                   select m;
                if (ultOperacion.ToList().Count == 0)
                {
                    var oOperacion = db.Operaciones.Find(idOperacion);
                    int idModulo = oOperacion.id_mod;
                    nomOperacion = getNombreOperacion(idOperacion);
                    nomModulo = getNombreModulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nomOperacion + "&modulo=" + nomModulo + "&mensajeExcepcion= ");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nomOperacion + "&modulo=" + nomModulo + "&mensajeExcepcion=" + ex.Message);
            }
        }

        public string getNombreOperacion(int idOperacion)
        {
            var ope = from op in db.Operaciones
                      where op.id_opera == idOperacion
                      select op.nombre_Tarea;
            String nombreOperacion;
            try
            {
                nombreOperacion = ope.First();
            }
            catch (Exception ex)
            {
                nombreOperacion = ex.Message;
            }
            return nombreOperacion;
        }
        public string getNombreModulo(int idModulo)
        {
            var modulo = from m in db.modulo
                         where m.id_modulo == idModulo
                         select m.nombre_modulo;
            String nombreModulo;
            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception ex)
            {
                nombreModulo = ex.Message;
            }
            return nombreModulo;
        }

    }
}