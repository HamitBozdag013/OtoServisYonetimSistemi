using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Web
{
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly Repository<Slider> repositorySlider = new Repository<Slider>();
        public ActionResult Index()
        {
            var slider = repositorySlider.List().OrderByDescending(s=>s.Id);
            return View(slider);
        }
        [HttpPost]
        public ActionResult SliderKaydet(Slider slider,HttpPostedFileBase resimYolu)
        {
            try
            {
                if (resimYolu != null)
                {
                    string uzanti = Path.GetExtension(resimYolu.FileName);
                    string dosyaAdi = Path.GetFileNameWithoutExtension(resimYolu.FileName) + "_" + Guid.NewGuid() +uzanti;
                    string yol = Server.MapPath("/Image/Slider/") + dosyaAdi;
                    resimYolu.SaveAs(yol);

                    string kaydedilecekYol = "/Image/Slider/" + dosyaAdi;
                    slider.ResimYolu = kaydedilecekYol;
                    repositorySlider.Add(slider);
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
        public ActionResult SliderSil(int id)
        {
            try
            {
                var silinecekSlider = repositorySlider.GetById(id);
                string resimYolu = Request.MapPath(silinecekSlider.ResimYolu);
                repositorySlider.Delete(silinecekSlider);
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