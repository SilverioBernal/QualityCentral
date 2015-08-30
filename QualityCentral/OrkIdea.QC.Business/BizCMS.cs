using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizCMS
    {
        //public List<Page> GetPages()
        //{
        //    return CRUDPage.GetPageList();
        //}

        //public List<Page> GetPages(int customerId)
        //{
        //    return CRUDPage.GetPageList(customerId);
        //}

        //public void SavePage(Page page)
        //{
        //    CRUDPage.SavePage(page);
        //}

        //public void DeletePage(string pageId, int customerId)
        //{
        //    CRUDPage.DeletePage(pageId, customerId);
        //}



        //public List<SideBar> GetSideBars()
        //{
        //    return CRUDSideBar.GetSideBarList();
        //}

        //public List<SideBar> GetSideBars(int customerId)
        //{
        //    return CRUDSideBar.GetSideBarList(customerId);
        //}

        //public void SaveSideBar(SideBar sideBar)
        //{
        //    CRUDSideBar.SaveSideBar(sideBar);
        //}

        //public void DeleteSideBar(int sideBarId)
        //{
        //    SideBar sideBar = CRUDSideBar.GetSideBarByKey(sideBarId);            
        //    int customerId = 0;
            
        //    if (sideBar != null)
        //    {
        //        customerId = sideBar.idCliente;

        //        List<Page> lsPages = GetPages(customerId);

        //        foreach (Page item in lsPages)
        //        {
        //            CRUDSideBar.DeleteSideBar(sideBarId);

        //            if (item.idSideBarDerecho.Equals(sideBarId))
        //            {
        //                item.idSideBarDerecho = null;
        //                CRUDPage.SavePage(item);
        //            }

        //            if (item.idSideBarIzquierdo.Equals(sideBarId))
        //            {
        //                item.idSideBarIzquierdo = null;
        //                CRUDPage.SavePage(item);
        //            }
        //        }
        //    }
            
        //}



        //public CMSPage GetCMSPage(string pageId, int customerId)
        //{
        //    CMSPage oCMSPage = null;

        //    try
        //    {
        //        Page page = CRUDPage.GetPageByKey(pageId, customerId);

        //        oCMSPage = new CMSPage()
        //        {
        //            idCliente = page.idCliente,
        //            id = page.id,
        //            titulo = page.titulo,
        //            idCarrusel = page.idCarrusel,
        //            imagenPrincipal = page.imagenPrincipal,
        //            contenido = page.contenido,
        //            keyWord = page.keyWord,
        //            metaData = page.metaData,
        //            leftSideBar = page.idSideBarIzquierdo != null ? CRUDSideBar.GetSideBarByKey((int)page.idSideBarIzquierdo) : null,
        //            rightSideBAr = page.idSideBarDerecho != null ? CRUDSideBar.GetSideBarByKey((int)page.idSideBarDerecho) : null,
        //            privado = page.privado,
        //            visible = page.visible
        //        };
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return oCMSPage;
        //}
    }
}
