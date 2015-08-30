using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public static class BizGenericParameter
    {
        public static IList<GenericParameter> GetList()
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();
            return ec.GetAll();
        }

        public static GenericParameter GetSingle(int id)
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static GenericParameter GetSingle(string name)
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();
            return ec.GetSingle(c => c.nombre.Equals(name));
        }

        public static void Add(params GenericParameter[] genericParameters)
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();

            ec.Add(genericParameters);
        }

        public static void Update(params GenericParameter[] genericParameters)
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();

            ec.Update(genericParameters);
        }

        public static void Remove(params GenericParameter[] genericParameters)
        {
            EntityCRUD<GenericParameter> ec = new EntityCRUD<GenericParameter>();
            ec.Remove(genericParameters);
        }
    }
}
 