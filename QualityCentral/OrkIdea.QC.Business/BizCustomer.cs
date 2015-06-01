using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizCustomer
    {
        public List<Customer> GetCustomers()
        {
            return CRUDCustomer.GetCustomerList();
        }

        public Customer GetCustomer(int customerId)
        {
            return CRUDCustomer.GetCustomerByKey(customerId);
        }

        public void SaveCustomer(Customer customer)
        {
            CRUDCustomer.SaveCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            CRUDCustomer.DeleteCustomer(customerId);
        }

        public void EnableCustomer(int customerId)
        {
            Customer customer = GetCustomer(customerId);
            customer.activo = true;

            SaveCustomer(customer);
        }

        public void DisableCustomer(int customerId)
        {
            Customer customer = GetCustomer(customerId);
            customer.activo = false;

            SaveCustomer(customer);
        }
    }
}
