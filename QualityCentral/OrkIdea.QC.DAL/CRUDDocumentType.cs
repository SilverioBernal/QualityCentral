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
    public static class CRUDDocumentType
    {
        /*CRUD DocumentType*/

        public static List<DocumentType> GetDocumentTypeList()
        {

            List<DocumentType> lstDocumentType = new List<DocumentType>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocumentType = ctx.DocumentType.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocumentType;
        }

        public static List<DocumentType> GetDocumentTypeList(int customerId)
        {

            List<DocumentType> lstDocumentType = new List<DocumentType>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstDocumentType = ctx.DocumentType.Where(x=> x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstDocumentType;
        }

        public static DocumentType GetDocumentTypeByKey(int documentTypeId)
        {
            DocumentType oDocumentType = new DocumentType();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oDocumentType =
                        ctx.DocumentType.Where(x => x.id.Equals(documentTypeId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oDocumentType;
        }

        public static void SaveDocumentType(DocumentType documentType)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    DocumentType oDocumentType = GetDocumentTypeByKey(documentType.id);

                    if (oDocumentType != null)
                    {
                        // if exists then edit 
                        ctx.DocumentType.Attach(oDocumentType);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oDocumentType, documentType);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.DocumentType.Add(documentType);
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

        public static void DeleteDocumentType(int documentTypeId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    DocumentType oDocumentType = GetDocumentTypeByKey(documentTypeId);

                    if (oDocumentType != null)
                    {
                        // if exists then edit 
                        ctx.DocumentType.Attach(oDocumentType);
                        ctx.DocumentType.Remove(oDocumentType);
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
