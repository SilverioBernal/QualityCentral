using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public static class  BizCustomerParameter
    {
        public static IList<CustomerParameter> GetList()
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();
            return ec.GetAll();
        }

        public static IList<CustomerParameter> GetList(int customerId)
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();
            return ec.GetAll(c => c.idCliente.Equals(customerId));
        }

        public static CustomerParameter GetSingle(int id, int customerId)
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();
            return ec.GetSingle(c => c.id.Equals(id) && c.idCliente.Equals(customerId));
        }

        public static CustomerParameter GetSingle(string name, int customerId)
        {
            GenericParameter genericParameter = BizGenericParameter.GetSingle(name);
            return GetSingle(genericParameter.id, customerId);
        }

        public static void Add(params CustomerParameter[] customerParameters)
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();

            ec.Add(customerParameters);
        }

        public static void Update(params CustomerParameter[] customerParameters)
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();

            ec.Update(customerParameters);
        }

        public static void Remove(params CustomerParameter[] customerParameters)
        {
            EntityCRUD<CustomerParameter> ec = new EntityCRUD<CustomerParameter>();
            ec.Remove(customerParameters);
        }
    }
}
