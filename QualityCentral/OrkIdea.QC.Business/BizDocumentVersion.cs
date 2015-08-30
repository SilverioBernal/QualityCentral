using Orkidea.Framework.Azure;
using Orkidea.Framework.Security;
using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public static class BizDocumentVersion
    {
        public static IList<DocumentVersion> GetList()
        {
            EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();
            return ObfuscateDocuments(ec.GetAll());
        }

        public static IList<DocumentVersion> GetList(int documentId)
        {
            EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();
            return ObfuscateDocuments(ec.GetList(c => c.idDocumento.Equals(documentId)));
        }

        public static DocumentVersion GetSingle(int id)
        {
            EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();
            return ObfuscateDocuments(ec.GetSingle(c => c.id.Equals(id)));
        }

        public static MemoryStream GetSingle(DocumentVersion document)
        {
            string connString = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;
            string destinyContainer = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;

            Storage storage = new Storage(connString, destinyContainer);
            return storage.getFile(HexSerialization.HexToString(document.id));
        }

        public static void Add(DocumentVersion documentVersion, Stream file)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;
                string destinyContainer = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;

                Storage storage = new Storage(connString, destinyContainer);

                EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();

                // ID = deserializated: [guid].[extension]
                if (storage.uploadFile(file, HexSerialization.HexToString(documentVersion.id)))
                    ec.Add(documentVersion);
                else
                    throw new Exception("Upload file failed.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(params DocumentVersion[] documentVersion)
        {
            EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();

            ec.Update(documentVersion);
        }

        public static void Remove(params DocumentVersion[] documentVersion)
        {
            string connString = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;
            string destinyContainer = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;

            Storage storage = new Storage(connString, destinyContainer);
            EntityCRUD<DocumentVersion> ec = new EntityCRUD<DocumentVersion>();

            foreach (DocumentVersion item in documentVersion)
            {
                if (storage.deleteFile(HexSerialization.HexToString(item.id)))
                    ec.Remove(documentVersion);
            }


        }

        public static void SetOfficial(int id)
        {
            DocumentVersion documentVersion = GetSingle(id);

            IList<DocumentVersion> documentVersions = GetList(documentVersion.idDocumento);

            foreach (DocumentVersion item in documentVersions)
            {
                if (item.id.Equals(id))
                    documentVersion.versionOficial = true;
                else
                    documentVersion.versionOficial = false;

                Update(documentVersion);
            }
        }

        private static DocumentVersion ObfuscateDocuments(DocumentVersion document)
        {
            document.id = HexSerialization.StringToHex(document.id);
            return document;
        }

        private static IList<DocumentVersion> ObfuscateDocuments(IList<DocumentVersion> documents)
        {
            IList<DocumentVersion> returnList = null;
            foreach (DocumentVersion item in documents)
            {
                item.id = HexSerialization.StringToHex(item.id);
                returnList.Add(item);
            }

            return returnList;
        }
    }
}
