using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizRole
    {
        public List<Role> GetRoles()
        {
            return CRUDRole.GetRoleList();
        }

        public Role GetRole(int roleId)
        {
            return CRUDRole.GetRoleByKey(roleId);
        }

        public void SaveRole(Role role)
        {
            CRUDRole.SaveRole(role);
        }

        public void DisableRole(int roleId)
        {
            BizMenu bizMenu = new BizMenu();
            BizUserProfile bizUserProfile = new BizUserProfile();

            Role roleToDisable = CRUDRole.GetRoleByKey(roleId);

            if (roleToDisable != null)
            {
                bizMenu.DisableRoleMenu(roleId);

                List<UserProfile> lsUserProfile = bizUserProfile.GetUserProfiles(roleToDisable.idCliente, roleToDisable.id);

                foreach (UserProfile item in lsUserProfile)
                    bizUserProfile.SaveUserProfile(new UserProfile() { id = item.id }, BizUserProfile.UserProfileAction.Disable);

                roleToDisable.activo = false;
                CRUDRole.SaveRole(roleToDisable);
            }
            
        }

        public void DeleteRole(int roleId)
        {
            BizMenu bizMenu = new BizMenu();
            BizUserProfile bizUserProfile = new BizUserProfile();

            Role roleToDelete = CRUDRole.GetRoleByKey(roleId);

            if (roleToDelete != null)
            {
                List<UserMenu> lsRoleMenu = bizMenu.GetRoleMenu(roleToDelete.id);
                List<UserProfile> lsUserProfile = bizUserProfile.GetUserProfiles(roleToDelete.idCliente, roleToDelete.id);

                foreach (UserMenu item in lsRoleMenu)
                    bizMenu.DeleteRoleMenuItem(item.id, roleToDelete.id);

                foreach (UserProfile item in lsUserProfile)                
                    bizUserProfile.DeleteUserProfile(item.id);                

                CRUDRole.DeleteRole(roleId);
            }
        }
    }
}
