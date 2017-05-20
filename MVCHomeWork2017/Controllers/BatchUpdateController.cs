using MVCHomeWork2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork2017.Controllers
{
    public class BatchUpdateController : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        CustomerEntities db = new CustomerEntities();


        public ActionResult Index()
        {
            return View(repo.All());
        }
        // GET: CustomerData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var data = repo.GetSingleDataById(id);
            var data = db.客戶資料.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            else { return View(data); }

        }
        // GET: Clients
        public ActionResult BatchUpdate()
        {
            //ViewData.Model = repo
            return View(repo.All());
        }

        //private void GetClients()
        //{
        //    var client = db.Client.Include(c => c.Occupation).Take(10);
        //    ViewData.Model = client;

        //}

        //[HttpPost]
        //public ActionResult BatchUpdate(ClientBatchUpdateVM[] items)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var item in items)
        //        {
        //            var c = db.Client.Find(item.ClientId);
        //            c.FirstName = item.FirstName;
        //            c.MiddleName = item.MiddleName;
        //            c.LastName = item.LastName;
        //        }
        //        db.SaveChanges();
        //        return RedirectToAction("BatchUpdate");
        //    }
        //    GetClients();
        //    return View();
        //}
    }
}