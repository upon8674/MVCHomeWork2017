using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork2017.Models
{   
	public  class CustomerViewRepository : EFRepository<CustomerView>, ICustomerViewRepository
	{

	}

	public  interface ICustomerViewRepository : IRepository<CustomerView>
	{

	}
}