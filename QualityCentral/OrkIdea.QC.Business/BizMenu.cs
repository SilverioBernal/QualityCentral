using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizMenu
    {
        //public List<UserMenu> GetGenericMenu()
        //{
        //    List<GenericMenu> lsGenMenu = new List<GenericMenu>();
        //    List<UserMenu> lsMenu = new List<UserMenu>();
        //    try
        //    {
        //        lsGenMenu.AddRange(CRUDGenericMenu.GetGenericMenuList().OrderBy(x => x.id).ThenBy(x => x.idPadre).ToList());

        //        lsMenu.AddRange(BuildMenu(lsGenMenu));
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return lsMenu;
        //}
        //public List<UserMenu> GetGenericMenu(bool estate)
        //{
        //    List<GenericMenu> lsGenMenu = new List<GenericMenu>();
        //    List<UserMenu> lsMenu = new List<UserMenu>();

        //    try
        //    {
        //        lsGenMenu.AddRange(CRUDGenericMenu.GetGenericMenuList().Where(x => x.activo.Equals(estate)).OrderBy(x => x.id).ThenBy(x => x.idPadre).ToList());
        //        lsMenu.AddRange(BuildMenu(lsGenMenu));
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return lsMenu;
        //}
        //private GenericMenu GetGenericMenuItem(int itemId)
        //{
        //    return CRUDGenericMenu.GetGenericMenuByKey(itemId);
        //}
        //public void SaveGenericMenuItem(GenericMenu menuItem)
        //{
        //    GenericMenu originalItem = GetGenericMenuItem(menuItem.id);

        //    if (originalItem == null)
        //        CRUDGenericMenu.SaveGenericMenu(menuItem);
        //    else
        //    {
        //        if ((originalItem.tipo == "M" || originalItem.tipo == "S") && menuItem.tipo== "I")
        //        {
        //            List<GenericMenu> lsMenuChilds = CRUDGenericMenu.GetGenericMenuList(menuItem.id);

        //            foreach (GenericMenu item in lsMenuChilds)                    
        //                CRUDGenericMenu.DeleteGenericMenu(item.id);                    
        //        }

        //        CRUDGenericMenu.SaveGenericMenu(menuItem);
        //    }
        //}
        //public void DeleteGenericMenuItem(GenericMenu menuItem)
        //{
        //    List<GenericMenu> lsMenu = CRUDGenericMenu.GetGenericMenuList(menuItem.id);

        //    foreach (GenericMenu item in lsMenu)            
        //        DeleteGenericMenuItem(item);

        //    CRUDGenericMenu.DeleteGenericMenu(menuItem.id);            
        //}        

        //public List<UserMenu> GetRoleMenu(int role)
        //{
        //    List<RoleMenu> lsRoleMenu = new List<RoleMenu>();
        //    List<UserMenu> lsMenu = new List<UserMenu>();
        //    try
        //    {
        //        lsRoleMenu.AddRange(CRUDRoleMenu.GetRoleMenuList(role).OrderBy(x => x.id).ThenBy(x => x.idPadre).ToList());
        //        lsMenu.AddRange(BuildMenu(lsRoleMenu));
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return lsMenu;
        //}
        //public List<UserMenu> GetRoleMenu(int role, bool estate)
        //{
        //    List<RoleMenu> lsGenMenu = new List<RoleMenu>();
        //    List<UserMenu> lsMenu = new List<UserMenu>();

        //    try
        //    {
        //        lsGenMenu.AddRange(CRUDRoleMenu.GetRoleMenuList(role).Where(x => x.activo.Equals(estate)).OrderBy(x => x.id).ThenBy(x => x.idPadre).ToList());
        //        lsMenu.AddRange(BuildMenu(lsGenMenu));
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return lsMenu;
        //}
        //private RoleMenu GetRoleMenuItem(int itemId, int roleId)
        //{
        //    return CRUDRoleMenu.GetRoleMenuByKey(itemId, roleId);
        //}
        //public void SaveRoleMenuItem(RoleMenu menuItem)
        //{
        //    RoleMenu originalItem = GetRoleMenuItem(menuItem.id, menuItem.idRol);

        //    if (originalItem == null)
        //        CRUDRoleMenu.SaveRoleMenu(menuItem);
        //    else
        //    {
        //        if ((originalItem.tipo == "M" || originalItem.tipo == "S") && menuItem.tipo == "I")
        //        {
        //            List<RoleMenu> lsMenuChilds = CRUDRoleMenu.GetRoleMenuList(menuItem.idRol, menuItem.id);

        //            foreach (RoleMenu item in lsMenuChilds)
        //                CRUDRoleMenu.DeleteRoleMenu(item.id, item.idRol);
        //        }

        //        CRUDRoleMenu.SaveRoleMenu(menuItem);
        //    }
        //}
        //public void DeleteRoleMenuItem(int itemId, int roleId)
        //{
        //    List<RoleMenu> lsMenu = CRUDRoleMenu.GetRoleMenuList(roleId, itemId);

        //    foreach (RoleMenu item in lsMenu)
        //        DeleteRoleMenuItem(item.id, item.idRol);

        //    CRUDRoleMenu.DeleteRoleMenu(itemId, roleId);
        //}
        //public void DisableRoleMenu(int roleId)
        //{
        //    List<RoleMenu> lsMenu = CRUDRoleMenu.GetRoleMenuList(roleId);

        //    foreach (RoleMenu item in lsMenu)
        //    {
        //        item.activo = false;
        //        CRUDRoleMenu.SaveRoleMenu(item);
        //    }            
        //}        

        //private List<UserMenu> BuildMenu(List<GenericMenu> lsMenu)
        //{
        //    List<UserMenu> lsUserMenu = new List<UserMenu>();

        //    foreach (GenericMenu item in lsMenu)
        //    {
        //        if (item.tipo == "M")
        //            lsUserMenu.Add(new UserMenu()
        //            {
        //                activo = item.activo,
        //                etiqueta = item.etiqueta,
        //                id = item.id,
        //                idPadre = item.idPadre,
        //                publico = (bool)item.publico,
        //                tipo = item.tipo,
        //                url = item.url,
        //                lsHijos = BuildMenuChilds(lsMenu.Where(x => x.idPadre.Equals(item.id)).ToList())
        //            });
        //    }

        //    return lsUserMenu;
        //}
        //private List<UserMenu> BuildMenuChilds(List<GenericMenu> lsMenu)
        //{
        //    List<UserMenu> lsUserMenu = new List<UserMenu>();

        //    foreach (GenericMenu item in lsMenu)
        //    {
        //        lsUserMenu.Add(new UserMenu()
        //        {
        //            activo = item.activo,
        //            etiqueta = item.etiqueta,
        //            id = item.id,
        //            idPadre = item.idPadre,
        //            publico = (bool)item.publico,
        //            tipo = item.tipo,
        //            url = item.url,
        //            lsHijos = BuildMenuChilds(lsMenu.Where(x => x.idPadre.Equals(item.id)).ToList())
        //        });
        //    }

        //    return lsUserMenu;
        //}
        //private List<UserMenu> BuildMenu(List<RoleMenu> lsMenu)
        //{
        //    List<UserMenu> lsUserMenu = new List<UserMenu>();

        //    foreach (RoleMenu item in lsMenu)
        //    {
        //        if (item.tipo == "M")
        //            lsUserMenu.Add(new UserMenu()
        //            {
        //                activo = item.activo,
        //                etiqueta = item.etiqueta,
        //                id = item.id,
        //                idPadre = item.idPadre,
        //                tipo = item.tipo,
        //                url = item.url,
        //                lsHijos = BuildMenuChilds(lsMenu.Where(x => x.idPadre.Equals(item.id)).ToList())
        //            });
        //    }

        //    return lsUserMenu;
        //}
        //private List<UserMenu> BuildMenuChilds(List<RoleMenu> lsMenu)
        //{
        //    List<UserMenu> lsUserMenu = new List<UserMenu>();

        //    foreach (RoleMenu item in lsMenu)
        //    {
        //        lsUserMenu.Add(new UserMenu()
        //        {
        //            activo = item.activo,
        //            etiqueta = item.etiqueta,
        //            id = item.id,
        //            idPadre = item.idPadre,
        //            tipo = item.tipo,
        //            url = item.url,
        //            lsHijos = BuildMenuChilds(lsMenu.Where(x => x.idPadre.Equals(item.id)).ToList())
        //        });
        //    }

        //    return lsUserMenu;
        //}
        //private List<UserMenu> BuildMenuChilds(GenericMenu menuItem)
        //{
        //    List<UserMenu> lsUserMenu = new List<UserMenu>();
        //    List<GenericMenu> lsMenu = CRUDGenericMenu.GetGenericMenuList(menuItem.id);

        //    foreach (GenericMenu item in lsMenu)
        //    {
        //        lsUserMenu.Add(new UserMenu()
        //        {
        //            activo = item.activo,
        //            etiqueta = item.etiqueta,
        //            id = item.id,
        //            idPadre = item.idPadre,
        //            tipo = item.tipo,
        //            url = item.url,
        //            lsHijos = BuildMenuChilds(item)
        //        });
        //    }

        //    return lsUserMenu;
        //}
        
    }
}
