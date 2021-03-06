﻿using System;
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
    public class CustomerBankController : Controller
    {
        //private CustomerEntities db = new CustomerEntities();
        客戶資料Repository repoCustomerData = RepositoryHelper.Get客戶資料Repository();
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();

        // GET: CustomerBank
        public ActionResult Index(string keyWord)
        {
            return View(repo.GetDataList(keyWord));
        }
      

        // GET: CustomerBank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetSingleDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            else { return View(data); }
        }

        // GET: CustomerBank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomerData.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerBank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomerData.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: CustomerBank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetSingleDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.客戶Id = new SelectList(repoCustomerData.All(), "Id", "客戶名稱", data.客戶Id);
                return View(data);
            }
        }

        // POST: CustomerBank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }            
            ViewBag.客戶Id = new SelectList(repoCustomerData.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: CustomerBank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = repo.GetSingleDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            else { return View(data); }
        }

        // POST: CustomerBank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 data = repo.GetSingleDataById(id);
            repo.Delete(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
