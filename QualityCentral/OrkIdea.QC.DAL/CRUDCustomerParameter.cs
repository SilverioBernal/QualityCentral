using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.DAL
{
    public static class CRUDCustomerParameter
    {
        public static List<CustomerParameter> GetCustomerParameterList()
        {

            List<CustomerParameter> lstCustomerParameter = new List<CustomerParameter>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstCustomerParameter = ctx.CustomerParameter.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstCustomerParameter;
        }

        public static List<CustomerParameter> GetCustomerParameterList(int customerId)
        {

            List<CustomerParameter> lstCustomerParameter = new List<CustomerParameter>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstCustomerParameter = ctx.CustomerParameter.Where(x => x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstCustomerParameter;
        }

        public static CustomerParameter GetCustomerParameterByKey(int customerParameterId, int customerId)
        {
            CustomerParameter oCustomerParameter = new CustomerParameter();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oCustomerParameter = ctx.CustomerParameter.Where(x => x.id.Equals(customerParameterId) && x.idCliente.Equals(customerId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oCustomerParameter;
        }

        public static void SaveCustomerParameter(CustomerParameter customerParameter)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    CustomerParameter oCustomerParameter = GetCustomerParameterByKey(customerParameter.id, customerParameter.idCliente);

                    if (oCustomerParameter != null)
                    {
                        // if exists then edit 
                        ctx.CustomerParameter.Attach(oCustomerParameter);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oCustomerParameter, customerParameter);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.CustomerParameter.Add(customerParameter);
                        ctx.SaveChanges();
                    }
                }

            }
            catch (DbEntityValidationException e)
            {
                StringBuilder oError = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    oError.AppendLine(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        oError.AppendLine(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                string msg = oError.ToString();
                throw new Exception(msg);
            }
            catch (Exception ex) { throw ex; }
        }

        public static void DeleteCustomerParameter(int customerParameterId, int customerId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    CustomerParameter oCustomerParameter = GetCustomerParameterByKey(customerParameterId,customerId);

                    if (oCustomerParameter != null)
                    {
                        // if exists then edit 
                        ctx.CustomerParameter.Attach(oCustomerParameter);
                        ctx.CustomerParameter.Remove(oCustomerParameter);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    throw new Exception("No se puede eliminar esta sede porque existe información asociada a esta.");
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
