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
    public class IletisimController : Controller
    {
        private readonly Repository<Iletisim> repositoryIletisim = new Repository<Iletisim>();

        public ActionResult Index()
        {
            if (repositoryIletisim.List().Count == 0)
            {
                Iletisim _iletisim = new Iletisim();
                _iletisim.Unvan = "-";
                _iletisim.Harita = "-";
                _iletisim.IletisimBilgi = "-";
                repositoryIletisim.Add(_iletisim);
            }
            var iletisim = repositoryIletisim.List().FirstOrDefault();
            return View(iletisim);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Kaydet(Iletisim iletisim)
        {
            if (repositoryIletisim.List().Count > 0)
            {
                var guncellenecek = repositoryIletisim.GetById(iletisim.Id);
                guncellenecek.Unvan = iletisim.Unvan;
                guncellenecek.Harita = iletisim.Harita;
                guncellenecek.IletisimBilgi = iletisim.IletisimBilgi;
                repositoryIletisim.Update(guncellenecek);
                TempData["Ok"] = "Kayıt başarılı.";
            }

            return RedirectToAction("Index");
        }
    }
}