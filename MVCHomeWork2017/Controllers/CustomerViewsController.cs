using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork2017.Models;

namespace MVCHomeWork2017.Controllers
{
    public class CustomerViewsController : Controller
    {
        private CustomerEntities db = new CustomerEntities();

        // GET: CustomerViews
        public ActionResult Index()
        {
            return View(db.CustomerView.ToList());
        }

        // GET: CustomerViews/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerView customerView = db.CustomerView.Find(id);
            if (customerView == null)
            {
                return HttpNotFound();
            }
            return View(customerView);
        }

        // GET: CustomerViews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerViews/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] CustomerView customerView)
        {
            if (ModelState.IsValid)
            {
                db.CustomerView.Add(customerView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerView);
        }

        // GET: CustomerViews/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerView customerView = db.CustomerView.Find(id);
            if (customerView == null)
            {
                return HttpNotFound();
            }
            return View(customerView);
        }

        // POST: CustomerViews/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] CustomerView customerView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerView);
        }

        // GET: CustomerViews/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerView customerView = db.CustomerView.Find(id);
            if (customerView == null)
            {
                return HttpNotFound();
            }
            return View(customerView);
        }

        // POST: CustomerViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerView customerView = db.CustomerView.Find(id);
            db.CustomerView.Remove(customerView);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
