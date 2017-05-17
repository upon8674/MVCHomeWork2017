using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVCHomeWork2017.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => !p.IsDelete);
        }

        public IQueryable<客戶聯絡人> All(bool showAll)
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

        public 客戶聯絡人 GetSingleDataById(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶聯絡人> GetDataList(string keyWord)
        {

            IQueryable<客戶聯絡人> all = this.All();
            if (String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => !p.IsDelete);
            }
            else
            {
                all = base.All().Where(p => p.姓名.Contains(keyWord.Trim()) && !p.IsDelete)
                    .OrderByDescending(p => p.Id);
            }
            return all;

        }

        public void Update(客戶聯絡人 客戶聯絡人)
        {
            this.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
        }

        //public IQueryable
        public override void Delete(客戶聯絡人 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            entity.IsDelete = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}