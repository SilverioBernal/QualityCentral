using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using OrkIdea.QC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizUserProfile
    {
        public enum UserProfileAction
        {
            New,AdminReset,UserReset,Disable,Enable
        }
        public List<UserProfile> GetUserProfiles()
        {
            return CRUDUserProfile.GetUserProfileList();
        }

        public List<UserProfile> GetUserProfiles(int customerId)
        {
            return CRUDUserProfile.GetUserProfileList(customerId);
        }

        public List<UserProfile> GetUserProfiles(int customerId, int roleId)
        {
            return GetUserProfiles(customerId).Where(x => x.idRol.Equals(roleId)).ToList();
        }

        public UserProfile GetUserProfile(int userProfileId)
        {
            return CRUDUserProfile.GetUserProfileByKey(userProfileId);
        }        

        public void SaveUserProfile(UserProfile userProfile, UserProfileAction action)
        {
            switch (action)
            {
                case UserProfileAction.New:
                    userProfile.clave = Cryptography.Encrypt(PasswordHelper.Generate());
                    break;
                case UserProfileAction.AdminReset:
                    userProfile.clave = Cryptography.Encrypt(PasswordHelper.Generate());
                    break;
                case UserProfileAction.UserReset:
                    userProfile.clave = Cryptography.Encrypt(userProfile.clave);
                    break;
                case UserProfileAction.Disable:
                    //userProfile.clave = Cryptography.Encrypt(PasswordHelper.Generate());
                    userProfile.activo = false;
                    break;
                case UserProfileAction.Enable:
                    //userProfile.clave = Cryptography.Encrypt(PasswordHelper.Generate());
                    userProfile.activo = true;
                    break;
                default:
                    break;
            }

            CRUDUserProfile.SaveUserProfile(userProfile);

            SendUserProfileNotification(userProfile, action);
        }

        private void SendUserProfileNotification(UserProfile userProfile, UserProfileAction action)
        {
            MailAddress to = new MailAddress(userProfile.email, string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));

            //MailingHelper.SendMail()
        }

        public void DeleteUserProfile(int userProfileId)
        {
            CRUDUserProfile.DeleteUserProfile(userProfileId);
        }       
    }
}
