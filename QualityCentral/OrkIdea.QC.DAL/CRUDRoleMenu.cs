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
    public static class CRUDRoleMenu
    {
        /*CRUD RoleMenu*/

        public static List<RoleMenu> GetRoleMenuList()
        {

            List<RoleMenu> lstRoleMenu = new List<RoleMenu>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstRoleMenu = ctx.RoleMenu.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstRoleMenu;
        }

        public static List<RoleMenu> GetRoleMenuList(int roleId)
        {

            List<RoleMenu> lstRoleMenu = new List<RoleMenu>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstRoleMenu = ctx.RoleMenu.Where(x => x.idRol.Equals(roleId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstRoleMenu;
        }

        public static List<RoleMenu> GetRoleMenuList(int roleId, int parentId)
        {

            List<RoleMenu> lstRoleMenu = new List<RoleMenu>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstRoleMenu = ctx.RoleMenu.Where(x => x.idPadre== parentId && x.idRol.Equals(roleId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstRoleMenu;
        }

        public static RoleMenu GetRoleMenuByKey(int roleMenuId, int roleId)
        {
            RoleMenu oRoleMenu = new RoleMenu();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oRoleMenu =
                        ctx.RoleMenu.Where(x => x.id.Equals(roleMenuId) && x.idRol.Equals(roleId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oRoleMenu;
        }

        public static void SaveRoleMenu(RoleMenu roleMenu)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    RoleMenu oRoleMenu = GetRoleMenuByKey(roleMenu.id,roleMenu.idRol);

                    if (oRoleMenu != null)
                    {
                        // if exists then edit 
                        ctx.RoleMenu.Attach(oRoleMenu);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oRoleMenu, roleMenu);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.RoleMenu.Add(roleMenu);
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

        public static void DeleteRoleMenu(int roleMenuId, int roleId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    RoleMenu oRoleMenu = GetRoleMenuByKey(roleMenuId,roleId);

                    if (oRoleMenu != null)
                    {
                        // if exists then edit 
                        ctx.RoleMenu.Attach(oRoleMenu);
                        ctx.RoleMenu.Remove(oRoleMenu);
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
