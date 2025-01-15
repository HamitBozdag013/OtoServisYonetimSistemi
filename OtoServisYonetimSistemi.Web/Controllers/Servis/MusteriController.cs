using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Servis
{
    public class MusteriController : Controller
    {
        private readonly Repository<Musteri> repositoryMusteri = new Repository<Musteri>();
        public ActionResult Index()
        {
            return View(repositoryMusteri.Get().OrderByDescending(m => m.Id).Take(20).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Musteri musteri)
        {
            repositoryMusteri.Add(musteri);
            TempData["Ok"] = "Müşteri eklendi.";
            return RedirectToAction("Index");
        }
        public ActionResult MusteriDuzenle(int id)
        {
            var musteri = repositoryMusteri.GetById(id);
            return View(musteri);
        }
        [HttpPost]
        public ActionResult MusteriDuzenle(Musteri musteri)
        {
            var guncellenecekMusteri = repositoryMusteri.GetById(musteri.Id);
            guncellenecekMusteri.AdSoyad = musteri.AdSoyad;
            guncellenecekMusteri.Telefon = musteri.Telefon;
            guncellenecekMusteri.Eposta = musteri.Eposta;
            guncellenecekMusteri.Adres = musteri.Adres;
            repositoryMusteri.Update(guncellenecekMusteri);
            TempData["Ok"] = "Müşteri bilgileri güncellendi.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult MusteriSil(int id)
        {
            var silinecekMusteri = repositoryMusteri.GetById(id);
            repositoryMusteri.Delete(silinecekMusteri);
            TempData["Ok"] = "Müşteri silinmiştir.";
            return RedirectToAction("Index");
        }
    }

}