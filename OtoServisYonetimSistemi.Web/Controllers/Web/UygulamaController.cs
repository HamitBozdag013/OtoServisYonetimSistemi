using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Web
{
    [Authorize(Roles = "Admin")]
    public class UygulamaController : Controller
    {
        private readonly Repository<Uygulama> repositoryUygulama = new Repository<Uygulama>();

        public ActionResult Index()
        {
            return View(repositoryUygulama.List().OrderByDescending(u=>u.Id));
        }
        [HttpPost]
        public ActionResult UygulamaKaydet(Uygulama uygulama, HttpPostedFileBase resimyolu)
        {
            try
            {
                if (resimyolu != null)
                {
                    string uzanti = Path.GetExtension(resimyolu.FileName);
                    string dosyaAdi = Path.GetFileNameWithoutExtension(resimyolu.FileName) + "_" + Guid.NewGuid() +uzanti;
                    string yol = Server.MapPath("/Image/Uygulamalar/") + dosyaAdi;
                    resimyolu.SaveAs(yol);

                    WebImage webImage = new WebImage(yol);
                    webImage.Resize(285, 180, true, true);
                    webImage.Save(yol);
                    
                    string kaydedilecekYol = "/Image/Uygulamalar/" + dosyaAdi;
                    uygulama.ResimYolu = kaydedilecekYol;
                    repositoryUygulama.Add(uygulama);
                    TempData["Ok"] = "Kayıt başarılı.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["No"] = "Hata oluştu.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult UygulamaSil(int id)
        {
            try
            {
                var silinecekUygulama = repositoryUygulama.GetById(id);
                string resimYolu = Request.MapPath(silinecekUygulama.ResimYolu);
                repositoryUygulama.Delete(silinecekUygulama);
                if (System.IO.File.Exists(resimYolu))
                {
                    System.IO.File.Delete(resimYolu);
                }
                TempData["Ok"] = "Silme işlemi tamamlandı.";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Ok"] = "Hata oluştu !!!";
                return RedirectToAction("Index");
            }
        }
    }
}