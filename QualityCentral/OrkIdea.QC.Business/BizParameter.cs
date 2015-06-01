using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public static class BizParameter
    {
        public static List<GenericParameter> GetGenericParameters()
        {
            return CRUDGenericParameter.GetGenericParameterList();
        }

        public static GenericParameter GetGenericParameter(int parameterId)
        {
            return GetGenericParameters().Where(x => x.id.Equals(parameterId)).FirstOrDefault();
        }

        public static GenericParameter GetGenericParameter(string parameterName)
        {
            return GetGenericParameters().Where(x => x.nombre == parameterName).FirstOrDefault();
        }

        public static void SaveGenericParameter(GenericParameter genericParameter)
        {
            CRUDGenericParameter.SaveGenericParameter(genericParameter);
        }

        public static void DeleteGenericParameter(int parameterId)
        {
            CRUDGenericParameter.DeleteGenericParameter(parameterId);
        }



        public static List<CustomerParameter> GetCustomerParameters(int customerId)
        {
            return CRUDCustomerParameter.GetCustomerParameterList(customerId);
        }

        public static CustomerParameter GetCustomerParameter(int parameterId, int customerId)
        {
            return CRUDCustomerParameter.GetCustomerParameterByKey(parameterId, customerId);
        }

        public static CustomerParameter GetCustomerParameter(int customerId, string parameterName)
        {
            GenericParameter parameter = GetGenericParameter(parameterName);
            return GetCustomerParameter(parameter.id,customerId);
        }

        public static void SaveCustomerParameter(CustomerParameter customerParameter)
        {
            CRUDCustomerParameter.SaveCustomerParameter(customerParameter);
        }

        public static void DeleteCustomerParameter(int parameterId, int customerId)
        {
            CRUDCustomerParameter.DeleteCustomerParameter(parameterId, customerId);
        }
    }
}
 