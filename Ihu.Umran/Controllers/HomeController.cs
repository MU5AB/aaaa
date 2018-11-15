using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ihu.Umran.DataDAL;
using Ihu.Umran.DataBLL.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Ihu.Umran.DataDAL.ModelsView;


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
        public ActionResult Editing_Popup()
        {
            return View();
        }

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicePRODUCTS a = new ServicePRODUCTS();
            return Json(a.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, VievPRODUCTS product)
        {
            if (product != null && ModelState.IsValid)
            {
                ServicePRODUCTS a = new ServicePRODUCTS();
                a.Create(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, VievPRODUCTS product)
        {
            if (product != null && ModelState.IsValid)
            {
                ServicePRODUCTS a = new ServicePRODUCTS();
                a.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, VievPRODUCTS product)
        {
            if (product != null)
            {
                ServicePRODUCTS a = new ServicePRODUCTS();
                a.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
    

}

}
