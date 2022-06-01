using System.Web;
using System.Web.Mvc;

namespace Detalle_Exquisito
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new Filtros.VerificaSession());
        }
    }
}
