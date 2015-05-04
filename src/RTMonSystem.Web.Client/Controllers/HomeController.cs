using RTMonSystem.Web.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTMonSystem.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Widgets()
        {
            var m = new HomeModel();
            m.WidgetList = new List<Widget>() 
            {
                new Widget("Widget 1", "Type 1", "Widget 1 Tiltle", "Widget 1 Description"),
                new Widget("Widget 2", "Type 1", "Widget 2 Tiltle", "Widget 2 Description"),
                new Widget("Widget 3", "Type 2", "Widget 3 Tiltle", "Widget 3 Description")
            };
            return Json(m, JsonRequestBehavior.AllowGet);
        }


    }
}