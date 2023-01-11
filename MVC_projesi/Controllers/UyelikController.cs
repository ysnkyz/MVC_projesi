using KutuphaneProgrami.Data.HelperClass;
using KutuphaneProgrami.Data.Model;
using KutuphaneProgrami.Data.UnitOfWork;
using MVC_projesi.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_projesi.Controllers
{
    public class UyelikController : Controller
    {
        UnitOfWork unitOfWork;

        public UyelikController()
        {
            unitOfWork = new UnitOfWork();
        }

        [YetkiKontrolSistemi]


        public ActionResult Index()
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki != null);
            return View(uyeler);
        }

        [YetkiKontrolSistemi]


        public ActionResult Ekle()
        {
            var uyeler = unitOfWork.GetRepository<Uye>().GetAll(x => x.Yetki == null);
            return View(uyeler);
        }

        [YetkiKontrolSistemi]


        [HttpPost]
        public JsonResult EkleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(parola) && !string.IsNullOrEmpty(parolaTekrar))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;
                    uye.Sifre = parola;
                    // 1 = admin , 2 = moderator , 3 = izleyici
                    uye.Yetki = "3";

                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }   
                else return Json("parolaUyusmazligi");
            }
            else return Json("bosOlamaz");
        }

        [YetkiKontrolSistemi]


        public ActionResult Guncelle(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);   
            return View(uye);
        }

        [YetkiKontrolSistemi]


        [HttpPost]
        public JsonResult GuncelleJson(int uyeId, string mail, string parola, string parolaTekrar)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                if (parola == parolaTekrar)
                {
                    parola = Sifreleme.Sifrele(parola);
                    var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
                    uye.Mail = mail;
                    if (!string.IsNullOrEmpty(parola))
                        uye.Sifre = parola;
                  
                    unitOfWork.GetRepository<Uye>().Update(uye);
                    unitOfWork.SaveChanges();
                    return Json("1");
                }
                else return Json("parolaUyusmazligi");
            }
            else return Json("mailBosOlamaz");
        }

        [YetkiKontrolSistemi]


        [HttpPost]
        public JsonResult SilJson(int uyeId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            unitOfWork.GetRepository<Uye>().Delete(uye);
            var durum = unitOfWork.SaveChanges();
            if (durum > 0)
                return Json("1");

            return Json("0");
        }

        [YetkiKontrolSistemi]

        [HttpPost]
        public JsonResult YetkiAtaJson(int uyeId, string yetkiId)
        {
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            uye.Yetki = yetkiId;
            unitOfWork.GetRepository<Uye>().Update(uye);
            var durum = unitOfWork.SaveChanges();
            if(durum > 0) return Json("1");

            return Json("0");

        }

        [YetkiKontrolSistemi]


        public ActionResult ProfilBilgilerimiGuncelle()
        {
            var uyeId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
            var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);
            return View(uye);
        }

        [HttpPost]
        public JsonResult ProfilBilgiGuncelleJson(string mail, string parola, string parolaTekrar, string tel)
        {
            if (string.IsNullOrEmpty(mail)) return Json("mailBosOlamaz");

            if (parola == parolaTekrar)
            {
                var uyeId = Convert.ToInt32(Request.Cookies["uye"]["Id"]);
                var uye = unitOfWork.GetRepository<Uye>().GetById(uyeId);

                uye.Mail = mail;

                if (!string.IsNullOrEmpty(parola))
                {
                    parola = Sifreleme.Sifrele(parola);
                    uye.Sifre = parola;
                }

                uye.Telefon = tel;

                unitOfWork.GetRepository<Uye>().Update(uye);
                unitOfWork.SaveChanges();
                return Json("1");
            }
            else return Json("parolaUyusmazligi");
        }
    }
}