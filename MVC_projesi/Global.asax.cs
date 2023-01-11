using MVC_projesi.HelperClasses;
using MVC_projesi.Tasks.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace MVC_projesi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CezaArttirmaDusurmeTrigger.Baslat();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // hatayı yakala
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;
            if (httpException != null)
            {
                // log bilgileri
                LogInfo log = new LogInfo
                {
                    Url = Request.Url.ToString(),
                    HataMesaji = httpException.Message,
                    EklenmeTarihi = DateTime.Now,
                    Ip = KullaniciIpAdres.KullaniciIPBul(),
                    Tarayici = Request.Browser.Browser,
                    Dil = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].Substring(0, 2)
                };

                switch (httpException.GetHttpCode())
                {
                    case 403:
                        Response.Redirect("/Hata/ErisimIzniYok");
                        break;
                    case 404:
                        Response.Redirect("/Hata/SayfaBulunamadi");
                        break;
                    case 500:
                        Response.Redirect("/Hata/SunucuHatasi");
                        break;
                    default:
                        Response.Redirect("/Hata/SayfaBulunamadi");
                        break;
                }
                Server.ClearError();
            }
        }
    }
}
