using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}