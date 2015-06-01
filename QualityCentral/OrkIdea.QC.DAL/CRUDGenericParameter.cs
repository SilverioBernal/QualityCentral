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
    public static class CRUDGenericParameter
    {
        public static List<GenericParameter> GetGenericParameterList()
        {

            List<GenericParameter> lstGenericParameter = new List<GenericParameter>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstGenericParameter = ctx.GenericParameter.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstGenericParameter;
        }

        public static GenericParameter GetGenericParameterByKey(int genericParameterId)
        {
            GenericParameter oGenericParameter = new GenericParameter();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oGenericParameter =
                        ctx.GenericParameter.Where(x => x.id.Equals(genericParameterId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oGenericParameter;
        }

        public static void SaveGenericParameter(GenericParameter genericParameter)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    GenericParameter oGenericParameter = GetGenericParameterByKey(genericParameter.id);

                    if (oGenericParameter != null)
                    {
                        // if exists then edit 
                        ctx.GenericParameter.Attach(oGenericParameter);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oGenericParameter, genericParameter);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.GenericParameter.Add(genericParameter);
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

        public static void DeleteGenericParameter(int genericParameterId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    GenericParameter oGenericParameter = GetGenericParameterByKey(genericParameterId);

                    if (oGenericParameter != null)
                    {
                        // if exists then edit 
                        ctx.GenericParameter.Attach(oGenericParameter);
                        ctx.GenericParameter.Remove(oGenericParameter);
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
