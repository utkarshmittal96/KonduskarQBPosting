using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Data;
using Konduskar_QuickBook.QuickBookAPI;

namespace Konduskar_QuickBook
{
    public class MasterAPI
    {
         public static void AddCustomer(string companyName,string title,string givenName,string familyName)
        {


            try
            {
                ServiceContext context = QuickBookConnection.GetDataServiceContext();
                var service = new DataService(context);

                Customer customer = new Customer();

                {
                    customer.CompanyName = companyName;
                    customer.GivenName = givenName;
                    customer.Title = title;
                    customer.FamilyName = familyName;
                }

                service.Add(customer);
            }
            catch (Intuit.Ipp.Exception.IdsException ex)
            {
                throw ex;
            }
        }

        public static Customer GetCustomer(string customerDisplaysName)
        {
            try
            {
                ServiceContext context = QuickBookConnection.GetDataServiceContext();
                var service = new DataService(context);
                Intuit.Ipp.QueryFilter.QueryService<Customer> queryServiceForCustomer = new Intuit.Ipp.QueryFilter.QueryService<Customer>(context);
                System.Collections.ObjectModel.ReadOnlyCollection<Customer> customerList = queryServiceForCustomer.ExecuteIdsQuery("select * from customer");
                Customer customer = customerList.First<Customer>(cust => cust.DisplayName == customerDisplaysName);
                return customer;
            }
            catch (Intuit.Ipp.Exception.IdsException ex)
            {
                throw;
            }
  
        }
    
    }
}