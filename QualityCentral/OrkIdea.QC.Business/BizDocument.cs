using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using OrkIdea.QC.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizDocument
    {
        public List<DocumentType> GetDocumentTypes()
        {
            return CRUDDocumentType.GetDocumentTypeList();
        }

        public List<DocumentType> GetDocumentTypes(int customerId)
        {
            return CRUDDocumentType.GetDocumentTypeList(customerId);
        }

        public List<DocumentType> GetDocumentTypes(int customerId, bool active)
        {
            return CRUDDocumentType.GetDocumentTypeList(customerId).Where(x => x.activo.Equals(active)).ToList();
        }

        public List<DocumentType> GetRecursiveDocumentTypesByProcess(int processId)
        {
            Process process = CRUDProcess.GetProcessByKey(processId);

            List<DocumentType> customerDocuments = new List<DocumentType>();

            List<DocumentType> customerDocumentTypes = GetDocumentTypes(process.idCliente);

            foreach (DocumentType item in customerDocumentTypes)
            {
                item.Document = GetDocumentsByProcess(processId); 

                if (item.Document.Count > 0)
                {
                    foreach (Document itemDocument in item.Document)                    
                        itemDocument.DocumentVersion = GetDocumentVersions(itemDocument.id);                    

                    customerDocuments.Add(item);
                }
            }

            return customerDocuments;
        }

        public List<DocumentType> GetRecursiveDocumentTypesByProcess(int processId, bool active)
        {
            Process process = CRUDProcess.GetProcessByKey(processId);

            List<DocumentType> customerDocuments = new List<DocumentType>();

            List<DocumentType> customerDocumentTypes = GetDocumentTypes(process.idCliente);

            foreach (DocumentType item in customerDocumentTypes)
            {
                item.Document = GetDocumentsByProcess(processId, active); 

                if (item.Document.Count > 0)
                {
                    foreach (Document itemDocument in item.Document)
                        itemDocument.DocumentVersion = GetDocumentVersions(itemDocument.id);

                    customerDocuments.Add(item);
                }
            }

            return customerDocuments;
        }

        public DocumentType GetDocumentType(int documentTypeId)
        {
            return CRUDDocumentType.GetDocumentTypeByKey(documentTypeId);
        }

        public void SaveDocumentType(DocumentType documentType)
        {
            CRUDDocumentType.SaveDocumentType(documentType);
        }

        public void DeleteDocumentType(int documentTypeId)
        {
            List<Document> lsDocumentosAsociados = GetDocumentsByType(documentTypeId);

            foreach (Document item in lsDocumentosAsociados)
            {
                List<DocumentVersion> archivos = CRUDDocumentVersion.GetDocumentVersionList(item.id);

                foreach (DocumentVersion archivo in archivos)                
                    DeleteDocumentVersion(archivo.id);

                CRUDDocument.DeleteDocument(item.id);
            }

            CRUDDocumentType.DeleteDocumentType(documentTypeId);
        }

        public void EnableDocumentType(int documentTypeId)
        {
            DocumentType documentType = GetDocumentType(documentTypeId);

            documentType.activo = true;

            SaveDocumentType(documentType);
        }

        public void DisableDocumentType(int documentTypeId)
        {
            List<Document> lsDocumentosAsociados = GetDocumentsByType(documentTypeId);

            foreach (Document item in lsDocumentosAsociados)            
                DisableDocument(item.id);            

            CRUDDocumentType.DeleteDocumentType(documentTypeId);
        }



        public List<Document> GetDocuments() 
        {
            return CRUDDocument.GetDocumentList();
        }

        public List<Document> GetDocumentsByType(int documentTypeId)
        {
            return CRUDDocument.GetDocumentList(documentTypeId);
        }

        public List<Document> GetDocumentsByProcess(int processId)
        {
            Process process = CRUDProcess.GetProcessByKey(processId);

            List<Document> customerDocuments = new List<Document>();

            List<DocumentType> customerDocumentTypes = GetDocumentTypes(process.idCliente);

            foreach (DocumentType item in customerDocumentTypes)
            {
                item.Document = CRUDDocument.GetDocumentList(item.id, process.id);

                if (item.Document.Count > 0)
                    customerDocuments.AddRange(item.Document);
            }

            return customerDocuments;
        }

        public List<Document> GetDocumentsByProcess(int processId, bool active)
        {
            Process process = CRUDProcess.GetProcessByKey(processId);

            List<Document> customerDocuments = new List<Document>();

            List<DocumentType> customerDocumentTypes = GetDocumentTypes(process.idCliente);

            foreach (DocumentType item in customerDocumentTypes)
            {
                item.Document = CRUDDocument.GetDocumentList(item.id, process.id).Where(x => x.activo.Equals(active)).ToList();

                if (item.Document.Count > 0)
                    customerDocuments.AddRange(item.Document);
            }

            return customerDocuments;
        }

        public Document GetDocument(int documentId)
        {
            return CRUDDocument.GetDocumentByKey(documentId);
        }

        public void SaveDocument(Document document)
        {
            CRUDDocument.SaveDocument(document);
        }

        public void EnableDocument(int documentId)
        {
            Document document = GetDocument(documentId);
            document.activo = true;
            SaveDocument(document);
        }

        public void DisableDocument(int documentId)
        {
            Document document = GetDocument(documentId);
            document.activo = false;

            SaveDocument(document);
        }


        public List<DocumentVersion> GetDocumentVersions(int documentId)
        {
            return CRUDDocumentVersion.GetDocumentVersionList(documentId);
        }

        public MemoryStream GetDocumentVersion(string documentVersionId)
        {
            return AzureStorageHelper.getFile(HexSerialization.FromHexString(documentVersionId), "uploadedFiles");
        }

        public void SaveDocumentVersion(DocumentVersion documentVersion, Stream file)
        {
            try
            {
                // uploadedFiles debe reemplazarse con la llamada a un parametro
                AzureStorageHelper.uploadFile(file, documentVersion.id, "uploadedFiles");

                documentVersion.id = HexSerialization.ToHexString(documentVersion.id);
                CRUDDocumentVersion.SaveDocumentVersion(documentVersion);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void DeleteDocumentVersion(string documentVersionId)
        {
            try
            {
                // uploadedFiles debe reemplazarse con la llamada a un parametro
                if (AzureStorageHelper.deleteFile(HexSerialization.FromHexString(documentVersionId), "uploadedFiles"))
                    CRUDDocumentVersion.DeleteDocumentVersion(documentVersionId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
