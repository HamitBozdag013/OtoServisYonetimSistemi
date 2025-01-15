using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers.Servis
{
    public class MarkaModelController : Controller
    {
        private readonly Repository<Model> repositoryModel = new Repository<Model>();
        private readonly Repository<Marka> repositoryMarka = new Repository<Marka>();
        // GET: MarkaModel
        public ActionResult Index()
        {
            return View(repositoryMarka.Get(m=>m.Silindi==false).OrderBy(m=>m.MarkaAd).ToList());
        }
        public JsonResult ModelDoldur(int markaId)
        {
            var modeller = repositoryModel.Get(m => m.MarkaId == markaId && m.Silindi==false).Select(m => new { m.Id, m.ModelAd }).ToList();
            return Json(modeller,JsonRequestBehavior.AllowGet);
        }
        public ActionResult MarkaKaydet(Marka marka)
        {
            if (repositoryMarka.Get(m=>m.MarkaAd==marka.MarkaAd).Any())
            {
                var _markaId = repositoryMarka.Get(m => m.MarkaAd == marka.MarkaAd).Select(m=>new { m.Id }).FirstOrDefault();
                var yenidenEklenecekMarka = repositoryMarka.GetById(_markaId.Id);
                if (yenidenEklenecekMarka.Silindi == true)
                {
                    yenidenEklenecekMarka.Silindi = false;
                    repositoryMarka.Update(yenidenEklenecekMarka);
                    TempData["Ok"] = "Marka tekrar eklenmiştir.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["No"] = "Bu marka zaten kayıtlı.";
                    return RedirectToAction("Index");
                }
            }
            repositoryMarka.Add(marka);
            TempData["Ok"] = "Yeni marka eklenmiştir.";
            return RedirectToAction("Index");
        }
        public ActionResult ModelListesi(int markaId)
        {
            var marka = repositoryMarka.GetById(markaId);
            ViewBag.Title = marka.MarkaAd + " Modelleri Listesi";
            ViewBag.MarkaId = markaId;
            return View(repositoryModel.Get(m=>m.MarkaId==markaId && m.Silindi==false).ToList());
        }
        public ActionResult ModelKaydet(Model model)
        {
            if (repositoryModel.Get(m => m.ModelAd == model.ModelAd).Any())
            {
                var _modelId = repositoryModel.Get(m => m.ModelAd == model.ModelAd).Select(m => new { m.Id }).FirstOrDefault();
                var yenidenEklenecekModel = repositoryModel.GetById(_modelId.Id);
                if (yenidenEklenecekModel.Silindi==true)
                {
                    yenidenEklenecekModel.Silindi = false;
                    repositoryModel.Update(yenidenEklenecekModel);
                    TempData["Ok"] = "Model tekrar eklenmiştir.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["No"] = "Bu model zaten kayıtlı.";
                    return RedirectToAction("Index");
                }       
            }
            repositoryModel.Add(model);
            TempData["Ok"] = "Yeni model eklenmiştir.";
            return RedirectToAction("ModelListesi",new { markaId=model.MarkaId});
        }
        [HttpPost]
        public ActionResult MarkaSil(int id)
        {
            var silinecekMarka = repositoryMarka.GetById(id);
            silinecekMarka.Silindi = true;
            repositoryMarka.Update(silinecekMarka);
            TempData["Ok"] = silinecekMarka.MarkaAd + " markası silindi.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ModelSil(int id)
        {
            var silinecekModel = repositoryModel.GetById(id);
            silinecekModel.Silindi = true;
            repositoryModel.Update(silinecekModel);
            TempData["Ok"] = silinecekModel.ModelAd + " modeli silindi.";
            return RedirectToAction("ModelListesi",new { markaId=silinecekModel.MarkaId});
        }
    }
}