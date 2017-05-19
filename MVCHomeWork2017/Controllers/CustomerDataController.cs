using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork2017.Models;
using PagedList;

namespace MVCHomeWork2017.Controllers
{
    public class CustomerDataController : Controller
    {
        //private CustomerEntities db = new CustomerEntities();

        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: CustomerData
        //public ActionResult Index()
        //{
        //    var data = db.客戶資料.Where(p=>!p.IsDelete).ToList();
        //    return View(data);
        //}
        //[HttpPost]
        //public ActionResult Index(string keyWord)
        //{
        //    var all = db.客戶資料.AsQueryable();
        //    var data = all
        //        .Where(p => p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete)                
        //        .OrderByDescending(p => p.Id).ToList();            
        //    return View(data);
        //}

        public ActionResult Index(string keyWord,string CustomerType, string columnName, string sortType, int page=1)
        {            
            if (string.IsNullOrEmpty(columnName))
            {
                //預設值
                ViewBag.columnName = "Id";
                ViewBag.sorttype = "asc";
                columnName = "Id";
            }
            else
            {
                //紀錄目前的排序欄位級升降冪屬性
                ViewBag.columnName = columnName;
                ViewBag.sorttype = sortType;               
            }


            var customerTypeData = repo.GetcustomerTypeList();            
            ViewBag.CustomerType = customerTypeData;
            
            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var data = repo.GetDataList(keyWord, CustomerType, columnName, sortType).ToPagedList(currentPage, pageSize);
            //var data1 = repo.GetDataList(keyWord, CustomerType, ViewBag.columnName+"", ViewBag.sorttype+"").ToPagedList(currentPage, pageSize);
            return View(data);
        }

//         #region -- SortColumnLink --
///// <summary>
///// sort column HyperLink.
///// </summary>
///// <param name="columnName">Name of the column.</param>
///// <returns></returns>
//private MvcHtmlString SortColumnLink(string columnName)
//{
//    string result = string.Format("<a class=\"sortColumnLink\" href=\"{0}\" id=\"{0}_{1}\">{2}</a>", columnName, "asc", columnName);
 
//    if (columnName.Equals(this.SortColumnName, StringComparison.OrdinalIgnoreCase))
//    {
//        string sortColumnLink = string.Format("<a class=\"sortColumnLink\" href=\"{0}\" id=\"{0}_{1}\" style=\"color: red;\">{2} {3}</a>",
//            columnName,
//            this.SortType,
//            columnName,
//            this.SortType.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "▲" : "▼");
 
//        result = sortColumnLink;
//    }
 
//    return MvcHtmlString.Create(result);
//} 
//#endregion



        // GET: CustomerData/Details/5
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

        // GET: CustomerData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerData/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,CustomerType")] 客戶資料 客戶資料)
        {         
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: CustomerData/Edit/5
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
            else { return View(data); }            
        }

        // POST: CustomerData/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,CustomerType")] 客戶資料 客戶資料)
        {
            //Todo  先撈取資料庫 確認是否有該ID的資料，才做下面的事
           if (ModelState.IsValid)
            {
                客戶資料.IsDelete = false;
                repo.Update(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: CustomerData/Delete/5
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

        // POST: CustomerData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
                repo.Delete(data);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
           


            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            //repo.Update(客戶資料);
            //repo.UnitOfWork.Commit();
            //return RedirectToAction("Index");                               
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
