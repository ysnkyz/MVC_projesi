using KutuphaneProgrami.Data.HelperClass;
using KutuphaneProgrami.Data.Model;
using KutuphaneProgrami.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_projesi.Controllers
{
    public class GirisController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public GirisController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            if (Request.Cookies["uye"] != null)
            
                return RedirectToAction("Index", "Kitap");
            
            return View();
        }

        [HttpPost]
        public JsonResult GirisKontrol(string email, string sifre, bool hatirla)
        {
            email = email.Trim();   
            sifre = sifre.Trim();
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(sifre))
                return Json("BosOlamaz");

            sifre = sifre.Sifrele();
            var uye = new Uye();

            try{ uye = _unitOfWork.GetRepository<Uye>().Get(x => x.Mail == email && x.Sifre == sifre);}
            catch { }

            if (uye != null)
            {
                HttpCookie cookie = new HttpCookie("uye");
                cookie.Values.Add("Id", uye.Id.ToString());
                cookie.Values.Add("Ad", uye.Ad);
                cookie.Values.Add("Soyad", uye.Soyad);
                cookie.Values.Add("YetkiId", uye.Yetki);

                if (hatirla) cookie.Expires = DateTime.Now.AddDays(5);

                Response.Cookies.Add(cookie);

                return Json("Başarılı");
            }
            else return Json("Hata");
        }

        public ActionResult CikisYap()
        {
            var cookie = Response.Cookies["uye"];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index");
        }
    }
}