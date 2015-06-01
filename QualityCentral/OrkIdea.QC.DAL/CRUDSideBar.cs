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
    public static class CRUDSideBar
    {
        /*CRUD SideBar*/

        public static List<SideBar> GetSideBarList()
        {

            List<SideBar> lstSideBar = new List<SideBar>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstSideBar = ctx.SideBar.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstSideBar;
        }

        public static List<SideBar> GetSideBarList(int customerId)
        {

            List<SideBar> lstSideBar = new List<SideBar>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstSideBar = ctx.SideBar.Where(x => x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstSideBar;
        }

        public static SideBar GetSideBarByKey(int sideBarId)
        {
            SideBar oSideBar = new SideBar();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oSideBar =
                        ctx.SideBar.Where(x => x.id.Equals(sideBarId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oSideBar;
        }

        public static void SaveSideBar(SideBar sideBar)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    SideBar oSideBar = GetSideBarByKey(sideBar.id);

                    if (oSideBar != null)
                    {
                        // if exists then edit 
                        ctx.SideBar.Attach(oSideBar);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oSideBar, sideBar);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.SideBar.Add(sideBar);
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

        public static void DeleteSideBar(int sideBarId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    SideBar oSideBar = GetSideBarByKey(sideBarId);

                    if (oSideBar != null)
                    {
                        // if exists then edit 
                        ctx.SideBar.Attach(oSideBar);
                        ctx.SideBar.Remove(oSideBar);
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
