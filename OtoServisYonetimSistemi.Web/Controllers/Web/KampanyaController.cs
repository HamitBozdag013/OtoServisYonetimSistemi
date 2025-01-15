using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Web
{
    [Authorize(Roles = "Admin")]
    public class KampanyaController : Controller
    {
        Repository<Kampanya> repositoryKampanya = new Repository<Kampanya>();

        public ActionResult Index()
        {
            if (repositoryKampanya.List().Count == 0)
            {
                Kampanya _kampanya = new Kampanya();
                _kampanya.Icerik = "-";
                repositoryKampanya.Add(_kampanya);
            }
            var kampanya = repositoryKampanya.List().FirstOrDefault();
            return View(kampanya);
        }
        [HttpPost]
        public ActionResult KampanyaKaydet(Kampanya kampanya)
        {
            if (repositoryKampanya.List().Count > 0)
            {
                var guncellenekIcerik = repositoryKampanya.List().FirstOrDefault();
                guncellenekIcerik.Icerik = kampanya.Icerik;
                repositoryKampanya.Update(guncellenekIcerik);
                TempData["Ok"] = "Kayıt başarılı.";
            }
            return RedirectToAction("Index");
        }
    }
}