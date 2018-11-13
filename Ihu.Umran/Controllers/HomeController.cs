using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ihu.Umran.DataDAL;
using Ihu.Umran.DataBLL.Services;

namespace Ihu.Umran.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           ServicePRODUCTS x = new ServicePRODUCTS();

            var model = x.Read();

                return View(model);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}