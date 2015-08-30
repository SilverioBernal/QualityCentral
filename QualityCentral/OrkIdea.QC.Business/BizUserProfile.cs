using Orkidea.Framework.Messaging;
using Orkidea.Framework.Security;
using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizUserProfile
    {
        public enum UserProfileAction { New, AdminReset, UserReset, Disable, Enable }

        public static IList<UserProfile> GetList()
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            return ec.GetAll();
        }

        public static IList<UserProfile> GetList(int customerId)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            return ec.GetAll(c => c.idCliente.Equals(customerId));
        }

        public static IList<UserProfile> GetList(int customerId, int roleId)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            return ec.GetAll(c => c.idCliente.Equals(customerId) && c.idRol.Equals(roleId));
        }

        public static UserProfile GetSingle(int id)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static UserProfile GetSingle(string userName)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            return ec.GetSingle(c => c.usuario.Equals(userName));
        }

        public static void Add(params UserProfile[] userProfiles)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();

            try
            {
                foreach (UserProfile item in userProfiles)
                {
                    item.clave = Cryptography.Encrypt(HexSerialization.StringToHex(PasswordHelper.Generate()));
                    ec.Add(userProfiles);

                    SendUserProfileNotification(item, UserProfileAction.New);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(UserProfileAction action, params UserProfile[] userProfiles)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();

            try
            {
                foreach (UserProfile item in userProfiles)
                {
                    switch (action)
                    {
                        case UserProfileAction.AdminReset:
                            item.clave = Cryptography.Encrypt(HexSerialization.StringToHex(PasswordHelper.Generate()));
                            break;
                        case UserProfileAction.UserReset:
                            item.clave = Cryptography.Encrypt(HexSerialization.StringToHex(item.clave));
                            break;
                        case UserProfileAction.Disable:
                            item.activo = false;
                            break;
                        case UserProfileAction.Enable:
                            item.activo = true;
                            break;
                        default:
                            break;
                    }

                    ec.Update(userProfiles);

                    SendUserProfileNotification(item, action);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Remove(params UserProfile[] userProfiles)
        {
            EntityCRUD<UserProfile> ec = new EntityCRUD<UserProfile>();
            ec.Remove(userProfiles);
        }

        private static void SendUserProfileNotification(UserProfile userProfile, UserProfileAction action)
        {
            int customerId = userProfile.idCliente;

            string host = Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("SmtpServer", customerId).valor));
            int port = int.Parse(Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("SmtpPort", customerId).valor)));
            bool enableSSL = bool.Parse(Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("SmtpSSL", customerId).valor)));
            string mailUser = Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("SmtpUser", customerId).valor));
            string mailPassword = Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("SmtpPassword", customerId).valor));
            string fromAlias = Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("MailingFromAlias", customerId).valor));
            string fromMail = Cryptography.Decrypt(HexSerialization.HexToString(BizCustomerParameter.GetSingle("MailingFromMail", customerId).valor));
            string mailLogo = BizCustomerParameter.GetSingle("LogoNotification", customerId).valor;

            Mailing mailing = new Mailing(host, port, enableSSL, mailUser, mailPassword);
            MailAddress from = new MailAddress(fromMail, fromAlias);
            List<MailAddress> to = new List<MailAddress>();
            to.Add(new MailAddress(userProfile.email, string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos)));

            List<LinkedResource> linkedResources = new List<LinkedResource>();
            linkedResources.Add(new LinkedResource(mailLogo, "image/png") { TransferEncoding = System.Net.Mime.TransferEncoding.Base64, ContentId = "LogoNotification" });

            Dictionary<string, string> dynamicValues = new Dictionary<string,string>();

            string subject = "";
            string htmlMessage = "", PlainTextMessage = "";

            switch (action)
            {
                case UserProfileAction.New:
                    subject = "QC - Notificación de creacion de usuario";
                    htmlMessage = BizCustomerParameter.GetSingle("HtmlNewUserNotification", customerId).valor;
                    PlainTextMessage = BizCustomerParameter.GetSingle("PlainTextNewUserNotification", customerId).valor;

                    dynamicValues.Add("[Name]", string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));
                    dynamicValues.Add("[User]", string.Format("{0}", userProfile.usuario));
                    dynamicValues.Add("[Password]", string.Format("{0}", Cryptography.Decrypt(HexSerialization.HexToString(userProfile.usuario))));
                    break;
                case UserProfileAction.AdminReset:
                    subject = "QC - Notificación de reestablecimiento de clave";
                    htmlMessage = BizCustomerParameter.GetSingle("HtmlAdminResetPasswordNotification", customerId).valor;
                    PlainTextMessage = BizCustomerParameter.GetSingle("PlainTextAdminResetPasswordNotification", customerId).valor;

                    dynamicValues.Add("[Name]", string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));                    
                    dynamicValues.Add("[Password]", string.Format("{0}", Cryptography.Decrypt(HexSerialization.HexToString(userProfile.usuario))));
                    break;
                case UserProfileAction.UserReset:
                    subject = "QC - Notificación de reestablecimiento de clave";
                    htmlMessage = BizCustomerParameter.GetSingle("HtmlUserResetPasswordNotification", customerId).valor;
                    PlainTextMessage = BizCustomerParameter.GetSingle("PlainTextUserResetPasswordNotification", customerId).valor;

                    dynamicValues.Add("[Name]", string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));                    
                    dynamicValues.Add("[Password]", string.Format("{0}", Cryptography.Decrypt(HexSerialization.HexToString(userProfile.usuario))));
                    break;
                case UserProfileAction.Disable:
                    subject = "QC - Notificación de inactivación de usuario";
                    htmlMessage = BizCustomerParameter.GetSingle("HtmlDisableUserNotification", customerId).valor;
                    PlainTextMessage = BizCustomerParameter.GetSingle("PlainTextDisableUserNotification", customerId).valor;

                    dynamicValues.Add("[Name]", string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));
                    dynamicValues.Add("[User]", string.Format("{0}", userProfile.usuario));
                    
                    break;
                case UserProfileAction.Enable:
                    subject = "QC - Notificación de reactivación de usuario";
                    htmlMessage = BizCustomerParameter.GetSingle("HtmlEnableUserNotification", customerId).valor;
                    PlainTextMessage = BizCustomerParameter.GetSingle("PlainTextEnableUserNotification", customerId).valor;

                    dynamicValues.Add("[Name]", string.Format("{0} {1}", userProfile.nombres, userProfile.apellidos));
                    dynamicValues.Add("[User]", string.Format("{0}", userProfile.usuario));                    
                    break;
                default:
                    break;
            }

            mailing.SendMail(from, to, subject, htmlMessage, PlainTextMessage, linkedResources, dynamicValues);
        }
    }
}
