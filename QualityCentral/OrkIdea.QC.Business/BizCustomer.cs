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
        public static IList<Customer> GetList()
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            return ec.GetAll();
        }

        public static IList<Customer> GetList(bool active)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            return ec.GetList(c => c.activo.Equals(active));
        }

        public static Customer GetSingle(int id)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static Customer GetSingle(string name)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            return ec.GetSingle(c => c.nombre.Equals(name));
        }        

        public static void Add(params Customer[] customers)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            
            ec.Add(customers);
        }

        public static void Update(params Customer[] customers)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();

            ec.Update(customers);
        }

        public static void Remove(params Customer[] customers)
        {
            EntityCRUD<Customer> ec = new EntityCRUD<Customer>();
            ec.Remove(customers);
        }

        public void Enable(params Customer[] customers)
        {
            foreach (Customer item in customers)
            {
                Customer customer = GetSingle(item.id);
                customer.activo = true;

                Update(customer);
            }
        }

        public void Disable(params Customer[] customers)
        {
            foreach (Customer item in customers)
            {
                Customer customer = GetSingle(item.id);
                customer.activo = false;

                Update(customer);
            }
        }        
    }
}
