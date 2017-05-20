using MVCHomeWork2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork2017.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
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
        public ActionResult GetJson()
        {
            CustomerEntities db = new CustomerEntities();
            db.Configuration.LazyLoadingEnabled = false;


            客戶資料Repository repoCustomerData = RepositoryHelper.Get客戶資料Repository();
            //repoCustomerData.All().
            return Json(repoCustomerData.All(),
                JsonRequestBehavior.AllowGet);
        }

      
    }
}