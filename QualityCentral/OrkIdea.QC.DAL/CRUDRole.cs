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
    public static class CRUDRole
    {
        /*CRUD Role*/

        public static List<Role> GetRoleList()
        {

            List<Role> lstRole = new List<Role>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstRole = ctx.Role.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstRole;
        }

        public static Role GetRoleByKey(int idRole)
        {
            Role oRole = new Role();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oRole =
                        ctx.Role.Where(x => x.id.Equals(idRole)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oRole;
        }

        public static void SaveRole(Role role)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    Role oRole = GetRoleByKey(role.id);

                    if (oRole != null)
                    {
                        // if exists then edit 
                        ctx.Role.Attach(oRole);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oRole, role);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.Role.Add(role);
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

        public static void DeleteRole(int idRole)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    Role oRole = GetRoleByKey(idRole);

                    if (oRole != null)
                    {
                        // if exists then edit 
                        ctx.Role.Attach(oRole);
                        ctx.Role.Remove(oRole);
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
