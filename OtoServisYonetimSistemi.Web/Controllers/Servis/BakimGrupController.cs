using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Servis
{
    public class BakimGrupController : Controller
    {
        private readonly Repository<Bakim> repositoryBakim = new Repository<Bakim>();
        private readonly Repository<BakimGrup> repositoryBakimGrup = new Repository<BakimGrup>();
        // GET: BakimGrup
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult BakimDoldur(int bakimGrupId)
        {
            var bakimGrup = repositoryBakim.Get(b => b.BakimGrupId ==  bakimGrupId).Select(b => new { b.Id, b.BakimAdi }).ToList();
            return Json(bakimGrup, JsonRequestBehavior.AllowGet);
        }
    }
}