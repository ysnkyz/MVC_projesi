using KutuphaneProgrami.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_projesi.HelperClasses
{
    public class YetkiKontrolSistemi : ActionFilterAttribute
    {
        readonly UnitOfWork _unitOfWork;

        public YetkiKontrolSistemi()
        {
            _unitOfWork = new UnitOfWork();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["uye"];
            if (cookie == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Giris" }, { "action", "Index" } });
            }
            else
            {
                var yetki = cookie["YetkiId"];
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                if (yetki == "2") // Yani Moderatör ise
                {
                    if (controllerName == "Uyelik")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Kitap" }, { "action", "Index" } });

                    }
                }
                else if (yetki == "3") // Yani İzleyici ise
                {
                    if (controllerName == "Uyelik" || actionName != "Index")
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Kitap" }, { "action", "Index" } });

                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}