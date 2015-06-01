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
    public static class CRUDPage
    {
        /*CRUD Page*/

        public static List<Page> GetPageList()
        {

            List<Page> lstPage = new List<Page>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstPage = ctx.Page.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstPage;
        }

        public static List<Page> GetPageList(int customerId)
        {

            List<Page> lstPage = new List<Page>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstPage = ctx.Page.Where(x => x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstPage;
        }

        public static Page GetPageByKey(string pageId, int customerId)
        {
            Page oPage = new Page();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oPage = ctx.Page.Where(x => x.id == pageId && x.idCliente.Equals(customerId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oPage;
        }

        public static void SavePage(Page page)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    Page oPage = GetPageByKey(page.id, page.idCliente);

                    if (oPage != null)
                    {
                        // if exists then edit 
                        ctx.Page.Attach(oPage);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oPage, page);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.Page.Add(page);
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

        public static void DeletePage(string pageId, int customerId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    Page oPage = GetPageByKey(pageId, customerId);

                    if (oPage != null)
                    {
                        // if exists then edit 
                        ctx.Page.Attach(oPage);
                        ctx.Page.Remove(oPage);
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
