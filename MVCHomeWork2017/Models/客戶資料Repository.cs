using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCHomeWork2017.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.IsDelete);
        }

        public IQueryable<客戶資料> All(bool showAll)
        {
            if (showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public 客戶資料 GetSingleDataById(int? id)
        {         
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶資料> Get客戶資料列表頁所有資料(string keyWord)
        {

            //if (!String.IsNullOrEmpty(keyWord))
            //{
            //    var all = db.客戶資料.AsQueryable();
            //    var data = all
            //        .Where(p => p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete)
            //        .OrderByDescending(p => p.Id).ToList();
            //    return View(data);
            //}
            //else
            //{
            //    var data = db.客戶資料.Where(p => !p.IsDelete).ToList();
            //    return View(data);
            //}


            IQueryable<客戶資料> all = this.All();
            if (String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => !p.IsDelete);
            }
            else
            {
                all = base.All().Where(p => p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete)
                    .OrderByDescending(p => p.Id);
            }
            return all;
               
        }

        public void Update(客戶資料 客戶資料)
        {
            this.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
        }

        //public IQueryable
        public override void Delete(客戶資料 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            entity.IsDelete = true;
        }




    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}