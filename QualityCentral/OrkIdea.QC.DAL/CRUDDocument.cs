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
    public static class CRUDDocument
    {
        /*CRUD Document*/

        public static List<Document> GetDocumentList()
        {

            List<Document> lstDocument = new List<Document>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocument = ctx.Document.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocument;
        }

        public static List<Document> GetDocumentList(int documentTypeId)
        {

            List<Document> lstDocument = new List<Document>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocument = ctx.Document.Where(x => x.idTipoDocumento.Equals(documentTypeId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocument;
        }

        public static List<Document> GetDocumentList(int documentTypeId, int processId)
        {

            List<Document> lstDocument = new List<Document>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocument = ctx.Document.Where(x => x.idTipoDocumento.Equals(documentTypeId) && x.idProceso.Equals(processId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocument;
        }

        public static Document GetDocumentByKey(int idDocument)
        {
            Document oDocument = new Document();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oDocument =
                        ctx.Document.Where(x => x.id.Equals(idDocument)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oDocument;
        }

        public static void SaveDocument(Document document)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    Document oDocument = GetDocumentByKey(document.id);

                    if (oDocument != null)
                    {
                        // if exists then edit 
                        ctx.Document.Attach(oDocument);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oDocument, document);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.Document.Add(document);
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

        public static void DeleteDocument(int idDocument)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    Document oDocument = GetDocumentByKey(idDocument);

                    if (oDocument != null)
                    {
                        // if exists then edit 
                        ctx.Document.Attach(oDocument);
                        ctx.Document.Remove(oDocument);
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
