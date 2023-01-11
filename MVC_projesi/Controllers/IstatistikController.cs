using KutuphaneProgrami.Data.Model;
using KutuphaneProgrami.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_projesi.Controllers
{
    public class IstatistikController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public IstatistikController()
        {
            _unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            ViewBag.KategoriSayisi = _unitOfWork.GetRepository<Kategori>().Count();
            ViewBag.YazarSayisi = _unitOfWork.GetRepository<Yazar>().Count();
            ViewBag.KitapSayisi = _unitOfWork.GetRepository<Kitap>().Count();
            ViewBag.TeslimEdilenKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x=>x.GetirdigiTarih == null);
            ViewBag.TeslimAlinanKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.GetirdigiTarih != null);

            var sonBirHafta = DateTime.Now.AddDays(-6);
            ViewBag.SonHaftaTeslimEdilenKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.AlisTarihi > sonBirHafta);
            ViewBag.SonHaftaTeslimAlinanKitapSayisi = _unitOfWork.GetRepository<OduncKitap>().Count(x => x.GetirdigiTarih != null && x.GetirdigiTarih > sonBirHafta);
            return View();
        }
    }
}