using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork2017.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => !p.IsDelete);
        }

        public IQueryable<客戶銀行資訊> All(bool showAll)
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

        public 客戶銀行資訊 GetSingleDataById(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶銀行資訊> GetDataList(string keyWord)
        {

            IQueryable<客戶銀行資訊> all = this.All();
            if (String.IsNullOrEmpty(keyWord))
            {
                all = base.All().Where(p => !p.IsDelete);
            }
            else
            {
                all = base.All().Where(p => p.銀行名稱.Contains(keyWord.Trim()) && !p.IsDelete)
                    .OrderByDescending(p => p.Id);
            }
            return all;

        }

        public void Update(客戶銀行資訊 客戶銀行資訊)
        {
            this.UnitOfWork.Context.Entry(客戶銀行資訊).State = System.Data.Entity.EntityState.Modified;
        }

        //public IQueryable
        public override void Delete(客戶銀行資訊 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            entity.IsDelete = true;
        }

    }

    public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}