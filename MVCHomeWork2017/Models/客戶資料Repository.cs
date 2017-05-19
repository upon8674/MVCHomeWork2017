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

        public IQueryable<客戶資料> GetDataList(string keyWord,string CustomerType,string columnName, string sorttype)
        {
                      
            IQueryable<客戶資料> all = this.All();
           

            if (String.IsNullOrEmpty(CustomerType) && String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => !p.IsDelete);
            }
            else if (String.IsNullOrEmpty(CustomerType) && !String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete);
            }
            else if (!String.IsNullOrEmpty(CustomerType) && String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.CustomerType.Contains(CustomerType.Trim()) && !p.IsDelete);
                    
            }
            else if (!String.IsNullOrEmpty(CustomerType) && !String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => p.CustomerType.Contains(CustomerType.Trim()) && p.客戶名稱.Contains(keyWord.Trim()) && !p.IsDelete);
            }

            
            //以反射的方式動態給LINQ TO Entity 的orderby條件
            var param = columnName;
            var propertyInfo = typeof(客戶資料).GetProperty(param);
            //因為IQueryable不支援C#方法  所以先轉型為Enumerable 根據動態條件排序完之後 再轉回IQueryable
            var allEnumerable = all.AsEnumerable();


            //排序
            switch (sorttype)
            {
                case "asc":
                    allEnumerable = allEnumerable.OrderBy(x => propertyInfo.GetValue(x, null));
                    break;
                case "desc":
                    allEnumerable = allEnumerable.OrderByDescending(x => propertyInfo.GetValue(x, null));
                    break;
                default:
                    allEnumerable = allEnumerable.OrderBy(x => propertyInfo.GetValue(x, null));
                    break;                    
            }
            return allEnumerable.AsQueryable();
               
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