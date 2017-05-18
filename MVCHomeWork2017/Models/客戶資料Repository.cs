using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using PagedList;

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

        public IQueryable<客戶資料> GetDataList(string keyWord,string CustomerType)
        {
                      
            IQueryable<客戶資料> all = this.All();
           

            if (String.IsNullOrEmpty(CustomerType) && String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => !p.IsDelete).OrderByDescending(p => p.Id);
            }
            else if (String.IsNullOrEmpty(CustomerType) && !String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete)
                  .OrderByDescending(p => p.Id);
            }
            else if (!String.IsNullOrEmpty(CustomerType) && String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.CustomerType.Contains(CustomerType.Trim()) && !p.IsDelete)
                    .OrderByDescending(p => p.Id);
            }
            else if (!String.IsNullOrEmpty(CustomerType) && !String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.CustomerType.Contains(CustomerType.Trim()) && p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete)
                    .OrderByDescending(p => p.Id);
            }

            return all;
               
        }

        //public List<SelectListItem> GetcustomerTypeList()
            public SelectList GetcustomerTypeList()
        {
            var ListData = base.All()
                .GroupBy(p => new { type = p.CustomerType })
                .Select(data => new 
                {
                    CustomerType = data.Key.type
                });

            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach (var type in ListData)
            //{
            //    items.Add(new SelectListItem()
            //    {
            //        Text=type.CustomerType,
            //        Value = type.CustomerType,
            //    });
            //}

            SelectList selectList = new SelectList(ListData, "CustomerType", "CustomerType");
            
            return selectList;
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