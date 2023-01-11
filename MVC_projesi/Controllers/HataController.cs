using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_projesi.Controllers
{
    public class HataController : Controller
    {
        // 404,400 hata sayfasları kısmı
        public ActionResult SayfaBulunamadi()
        {
            return View();
        }

        // 403 hatası kısmı
        public ActionResult ErisimIzniYok()
        {
            return View();
        }

        // 500
        public ActionResult SunucuHatasi()
        {
            return View();
        }
    }
}