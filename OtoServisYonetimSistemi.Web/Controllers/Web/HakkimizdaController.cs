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
    public class HakkimizdaController : Controller
    {
        private readonly Repository<Hakkimizda> repositoryHakkimizda = new Repository<Hakkimizda>();

        public ActionResult Index()
        {
            if (repositoryHakkimizda.List().Count == 0)
            {
                Hakkimizda _hakkimizda = new Hakkimizda();
                _hakkimizda.Icerik = "-";
                repositoryHakkimizda.Add(_hakkimizda);
            }
            return View(repositoryHakkimizda.List().FirstOrDefault());
        }
        [HttpPost]
        public ActionResult HakkimizdaKaydet(Hakkimizda hakkimizda, HttpPostedFileBase resimyolu)
        {
            try
            {
                if (repositoryHakkimizda.List().Count>0)
                {
                    var guncellenecekHakkimizda = repositoryHakkimizda.List().FirstOrDefault();
                    string silinecekHakkimizdaResimYolu = Request.MapPath(guncellenecekHakkimizda.ResimYolu);
                    if (System.IO.File.Exists(silinecekHakkimizdaResimYolu))
                    {
                        System.IO.File.Delete(silinecekHakkimizdaResimYolu);
                    }
                    

                    string uzanti = Path.GetExtension(resimyolu.FileName);
                    string dosyaAdi = Path.GetFileNameWithoutExtension(resimyolu.FileName) + "_" + Guid.NewGuid() + uzanti;
                    string yol = Server.MapPath("/Image/Hakkimizda/") + dosyaAdi;
                    resimyolu.SaveAs(yol);

                    WebImage webImage = new WebImage(yol);
                    webImage.Resize(640, 540, true, true);
                    webImage.Save(yol);

                    
                    guncellenecekHakkimizda.Icerik = hakkimizda.Icerik;
                    guncellenecekHakkimizda.ResimYolu = "/Image/Hakkimizda/" + dosyaAdi;
                    repositoryHakkimizda.Update(guncellenecekHakkimizda);
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
    }
}