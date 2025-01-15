using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Web;
using System.Linq;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<Slider> repositorySlider = new Repository<Slider>();
        private readonly Repository<Kampanya> repositoryKampanya = new Repository<Kampanya>();
        private readonly Repository<Uygulama> repositoryUygulama = new Repository<Uygulama>();
        private readonly Repository<Hakkimizda> repositoryHakkimizda = new Repository<Hakkimizda>();
        private readonly Repository<Blog> repositoryBlog = new Repository<Blog>();
        private readonly Repository<Iletisim> repositoryIletesim = new Repository<Iletisim>();


        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Slider = repositorySlider.List();
            ViewBag.Kampanya = repositoryKampanya.List().FirstOrDefault();
            ViewBag.Uygulama = repositoryUygulama.List();
            ViewBag.Hakkimizda = repositoryHakkimizda.List().FirstOrDefault();
            ViewBag.Blog = repositoryBlog.Get().Take(4).ToList();
            ViewBag.Harita = repositoryIletesim.List().FirstOrDefault();
            ViewBag.Adres = repositoryIletesim.List().FirstOrDefault().IletisimBilgi;
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Deneme()
        {
            return View();
        }
        [Route("{baslik}/{id:int}")]
        public ActionResult BlogDetay(string baslik, int id)
        {
            var detay = repositoryBlog.GetById(id);
            ViewBag.Adres = repositoryIletesim.List().FirstOrDefault().IletisimBilgi;
            return View(detay);
        }
    }
}
