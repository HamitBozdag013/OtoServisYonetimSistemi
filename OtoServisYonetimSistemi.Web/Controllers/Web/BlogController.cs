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
    public class BlogController : Controller
    {
        private readonly Repository<Blog> repositoryBlog = new Repository<Blog>();

        public ActionResult Index()
        {
            return View(repositoryBlog.List().OrderByDescending(b => b.Id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            try
            {
                blog.Tarih = DateTime.Now;
                repositoryBlog.Add(blog);
                TempData["Ok"] = "Kayıt başarılı.";
            }
            catch (Exception)
            {
                TempData["No"] = "Hata oluştu.";
            }
            return RedirectToAction("Index");
        }
        public ActionResult BlogSil(int id)
        {
            var silinecekBlog = repositoryBlog.GetById(id);
            return View(silinecekBlog);
        }
        [HttpPost]
        public ActionResult BlogSil(Blog blog)
        {
            var silinecekBlog = repositoryBlog.GetById(blog.Id);
            repositoryBlog.Delete(silinecekBlog);
            TempData["Ok"] = "Blog silindi.";
            return RedirectToAction("Index");
        }
        public ActionResult BlogDuzenle(int id)
        {
            return View(repositoryBlog.GetById(id));
        }
        [HttpPost]
        public ActionResult BlogDuzenle(Blog blog)
        {
            blog.Tarih = DateTime.Now;
            repositoryBlog.Update(blog);
            TempData["Ok"] = "Blog güncellendi.";
            return RedirectToAction("Index");
        }
    }
}

