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
    public static class CRUDUserProfile
    {
        /*CRUD UserProfile*/

        public static List<UserProfile> GetUserProfileList()
        {

            List<UserProfile> lstUserProfile = new List<UserProfile>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstUserProfile = ctx.UserProfile.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstUserProfile;
        }

        public static List<UserProfile> GetUserProfileList(int customerId)
        {

            List<UserProfile> lstUserProfile = new List<UserProfile>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstUserProfile = ctx.UserProfile.Where(x => x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstUserProfile;
        }

        public static UserProfile GetUserProfileByKey(int userProfileId)
        {
            UserProfile oUserProfile = new UserProfile();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oUserProfile = ctx.UserProfile.Where(x => x.id.Equals(userProfileId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oUserProfile;
        }

        public static void SaveUserProfile(UserProfile UserProfile)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    UserProfile oUserProfile = GetUserProfileByKey(UserProfile.id);

                    if (oUserProfile != null)
                    {
                        // if exists then edit 
                        ctx.UserProfile.Attach(oUserProfile);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oUserProfile, UserProfile);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.UserProfile.Add(UserProfile);
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

        public static void DeleteUserProfile(int userProfileId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    UserProfile oUserProfile = GetUserProfileByKey(userProfileId);

                    if (oUserProfile != null)
                    {
                        // if exists then edit 
                        ctx.UserProfile.Attach(oUserProfile);
                        ctx.UserProfile.Remove(oUserProfile);
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
