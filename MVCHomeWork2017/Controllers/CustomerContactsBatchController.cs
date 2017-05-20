using MVC5Course.Models.ViewModel;
using MVCHomeWork2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork2017.Controllers
{
    public class CustomerContactsBatchController : Controller
    {
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        // GET: CustomerContactsBatch
        public ActionResult Index(int? id)
        {
            
            return View(repo.All().Where(p => p.客戶Id == id));
        }
        [HttpPost]
        public ActionResult BatchUpdate(ContactsBatchUpdateVM[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var c = repo.GetSingleDataById(item.Id);
                    c.職稱 = item.職稱;
                    c.電話 = item.電話;
                    c.手機 = item.手機;
                }
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(repo.All().Where(p => p.客戶Id == items[0].客戶Id));
        }
    }
}