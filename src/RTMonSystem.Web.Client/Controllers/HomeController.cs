﻿using RTMonSystem.DataSources;
using RTMonSystem.DataSources.REST.Yahoo;
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
                new JSONWidget("Widget 1", typeof(YahooFinDataSource), "Yahoo Finance GOOG", "Yahoo Finance GOOG", 
                                path: "[0].query.results.quote.Bid", refreshRate: 300),
                new Widget("Widget 2", typeof(RandomNumberDataSource), "Sample Random Generator", "Sample Random Generator",
                                refreshRate: 300),
            };
            return Json(m, JsonRequestBehavior.AllowGet);
        }


    }
}