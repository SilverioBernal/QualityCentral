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
    public static class CRUDDocumentVersion
    {
        /*CRUD DocumentVersion*/

        public static List<DocumentVersion> GetDocumentVersionList()
        {

            List<DocumentVersion> lstDocumentVersion = new List<DocumentVersion>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocumentVersion = ctx.DocumentVersion.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocumentVersion;
        }

        public static List<DocumentVersion> GetDocumentVersionList(int documentId)
        {

            List<DocumentVersion> lstDocumentVersion = new List<DocumentVersion>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocumentVersion = ctx.DocumentVersion.Where(x => x.idDocumento.Equals(documentId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocumentVersion;
        }

        public static DocumentVersion GetDocumentVersionByKey(string idDocumentVersion)
        {
            DocumentVersion oDocumentVersion = new DocumentVersion();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oDocumentVersion =
                        ctx.DocumentVersion.Where(x => x.id.Equals(idDocumentVersion)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oDocumentVersion;
        }

        public static void SaveDocumentVersion(DocumentVersion documentVersion)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    DocumentVersion oDocumentVersion = GetDocumentVersionByKey(documentVersion.id);

                    if (oDocumentVersion != null)
                    {
                        // if exists then edit 
                        ctx.DocumentVersion.Attach(oDocumentVersion);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oDocumentVersion, documentVersion);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.DocumentVersion.Add(documentVersion);
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

        public static void DeleteDocumentVersion(string idDocumentVersion)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    DocumentVersion oDocumentVersion = GetDocumentVersionByKey(idDocumentVersion);

                    if (oDocumentVersion != null)
                    {
                        // if exists then edit 
                        ctx.DocumentVersion.Attach(oDocumentVersion);
                        ctx.DocumentVersion.Remove(oDocumentVersion);
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
