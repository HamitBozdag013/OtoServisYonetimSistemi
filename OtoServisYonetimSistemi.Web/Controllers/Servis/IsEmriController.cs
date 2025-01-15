using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Servis
{
    public class IsEmriController : Controller
    {
        private readonly Repository<Musteri> repositoryMusteri = new Repository<Musteri>();
        private readonly Repository<Marka> repositoryMarka = new Repository<Marka>();
        private readonly Repository<IsEmri> repositoryIsEmri = new Repository<IsEmri>();
        private readonly Repository<BakimGrup> repositoryBakimGrup = new Repository<BakimGrup>();
        private readonly Repository<Islem> repositoryIslem = new Repository<Islem>();
        // GET: IsEmri
        public ActionResult Index(string ara)
        {
            if (ara == "" || ara == null)
            {
                var musteri = repositoryMusteri.Get().OrderByDescending(m => m.Id).Take(20).ToList();
                return View(musteri);
            }
            var musteriAra = repositoryMusteri.Get(m => m.AdSoyad.Contains(ara) || m.Telefon.Contains(ara)).ToList();
            return View(musteriAra);
        }
        public ActionResult IsEmriOlustur(int musteriId)
        {
            ViewBag.Marka = repositoryMarka.Get(m => m.Silindi == false).ToList();
            ViewBag.MusteriId = musteriId;
            var musteri = repositoryMusteri.GetById(musteriId);
            ViewBag.Title = musteri.AdSoyad + " Adına İş Emri Oluştur ";
            ViewBag.AcikIsEmirleri = repositoryIsEmri.Get(i => i.Kapali == false && i.MusteriId == musteriId).ToList();
            return View(repositoryIsEmri.Get(i => i.Kapali == true && i.MusteriId == musteriId).OrderByDescending(i => i.GelisTarihi).ToList());
        }
        public ActionResult IsEmriKaydet(IsEmri ısEmri)
        {
            repositoryIsEmri.Add(ısEmri);
            return RedirectToAction("AcikIsEmirleri");
        }
        public ActionResult AcikIsEmirleri()
        {
            return View(repositoryIsEmri.Get(i => i.Kapali == false).ToList());
        }
        public ActionResult IslemYap(int isEmriId)
        {
            var baslik = repositoryIsEmri.Get(x => x.Id == isEmriId, includeProperties: "Musteri").FirstOrDefault();
            ViewBag.Title = "İşlemi yapılan müşteri: " + baslik.Musteri.AdSoyad + "  -  " + "Araç plakası: " + baslik.Plaka;
            ViewBag.BakimGrup = repositoryBakimGrup.List();
            ViewBag.IsEmriId = isEmriId;
            return View(repositoryIslem.Get(i => i.IsEmriId == isEmriId).OrderByDescending(i => i.Id).ToList());
        }
        public ActionResult IslemKaydet(Islem islem)
        {
            repositoryIslem.Add(islem);
            return RedirectToAction("IslemYap", new { isEmriId = islem.IsEmriId });
        }
        public ActionResult IslemSil(int id)
        {
            var silinecekIslem = repositoryIslem.GetById(id);
            repositoryIslem.Delete(silinecekIslem);
            return RedirectToAction("IslemYap", new { isEmriId = silinecekIslem.IsEmriId });
        }
        public ActionResult KaydetveKapat(int isEmriId, string OdemeSekli, string Aciklama, decimal AlinanUcret)
        {
            var isEmri = repositoryIsEmri.GetById(isEmriId);
            if (isEmri.Kapali)
            {
                TempData["No"] = "Bu İş emri zaten kapalıdır.";
                return RedirectToAction("AcikIsEmirleri");
            }
            isEmri.OdemeSekli = OdemeSekli;
            isEmri.Aciklama = Aciklama;
            isEmri.AlinanUcret = AlinanUcret;
            isEmri.Kapali = true;
            isEmri.BitisTarihi = DateTime.Now;
            repositoryIsEmri.Update(isEmri);
            TempData["Ok"] = "İş emri kaydedildi ve kapatıldı.";
            return RedirectToAction("RaporSayfasi",new { IsEmriId=isEmriId});
        }
        public ActionResult RaporSayfasi(int isEmriId)
        {
            ViewBag.IsEmriId = isEmriId;
            return View();
        }
        public ActionResult IsEmriOlusturSablon(int isEmriId)
        {        
            return View(repositoryIsEmri.GetById(isEmriId));
        }
        [HttpPost]
        public ActionResult IsEmriOlusturSablon(IsEmri isEmri)
        {
            var kapaliIsEmri = repositoryIsEmri.GetById(isEmri.Id);
            if (repositoryIsEmri.Get(i=>i.Plaka==kapaliIsEmri.Plaka && i.MusteriId==kapaliIsEmri.MusteriId&&i.Kapali==false).Count()>0)
            {
                TempData["No"] = "Bu Plaka için İş emri zaten açık.";
                return RedirectToAction("AcikIsEmirleri");
            }
            isEmri.MusteriId = kapaliIsEmri.MusteriId;
            isEmri.ModelId = kapaliIsEmri.ModelId;
            isEmri.ModelYil = kapaliIsEmri.ModelYil;
            isEmri.SaseNo = kapaliIsEmri.SaseNo;
            isEmri.Plaka = kapaliIsEmri.Plaka;
            isEmri.YakitTuru = kapaliIsEmri.YakitTuru;
            isEmri.Kapali = false;
            isEmri.Aciklama = null;
            isEmri.AlinanUcret = 0;
            isEmri.OdemeSekli = null;
            isEmri.BitisTarihi = null;
            repositoryIsEmri.Add(isEmri);
            TempData["Ok"] = "İş emri yeniden açıldı.";
            return RedirectToAction("AcikIsEmirleri");
        }
        public ActionResult Detay(int isEmriId)
        {
            ViewBag.Islemler = repositoryIslem.Get(i => i.IsEmriId == isEmriId).OrderByDescending(i => i.Id).ToList();
            return View(repositoryIsEmri.GetById(isEmriId));
        }

    }
}