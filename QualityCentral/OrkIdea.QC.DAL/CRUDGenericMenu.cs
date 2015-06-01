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
    public static class CRUDGenericMenu
    {
        public static List<GenericMenu> GetGenericMenuList()
        {

            List<GenericMenu> lstGenericMenu = new List<GenericMenu>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstGenericMenu = ctx.GenericMenu.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstGenericMenu;
        }

        public static List<GenericMenu> GetGenericMenuList(int parentId)
        {

            List<GenericMenu> lstGenericMenu = new List<GenericMenu>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstGenericMenu = ctx.GenericMenu.Where(x => x.idPadre==parentId).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstGenericMenu;
        }

        public static GenericMenu GetGenericMenuByKey(int genericMenuId)
        {
            GenericMenu oGenericMenu = new GenericMenu();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oGenericMenu = ctx.GenericMenu.Where(x => x.id.Equals(genericMenuId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oGenericMenu;
        }

        public static void SaveGenericMenu(GenericMenu genericMenu)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    GenericMenu oGenericMenu = GetGenericMenuByKey(genericMenu.id);

                    if (oGenericMenu != null)
                    {
                        // if exists then edit 
                        ctx.GenericMenu.Attach(oGenericMenu);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oGenericMenu, genericMenu);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.GenericMenu.Add(genericMenu);
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

        public static void DeleteGenericMenu(int genericMenuId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    GenericMenu oGenericMenu = GetGenericMenuByKey(genericMenuId);

                    if (oGenericMenu != null)
                    {
                        // if exists then edit 
                        ctx.GenericMenu.Attach(oGenericMenu);
                        ctx.GenericMenu.Remove(oGenericMenu);
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
