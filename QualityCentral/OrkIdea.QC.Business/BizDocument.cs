using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
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
        public static IList<Document> GetList()
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();
            return ec.GetAll();
        }

        public static IList<Document> GetList(int processId)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            Process pr = BizProcess.GetSingle(processId);

            return ec.GetList(c => c.idProceso.Equals(processId)).ToList();
        }

        public static IList<Document> GetList(int processId, bool active)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            Process pr = BizProcess.GetSingle(processId);

            return ec.GetList(c => c.idProceso.Equals(processId))
                .Where(c => c.activo.Equals(active)).ToList();            
        }

        public static IList<Document> GetList(int processId, int documentTypeId)
        {
            IList<Document> docs = null;

            Process pr = BizProcess.GetSingle(processId);

            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            IList<Document> docsPorTipo = ec.GetList(c => c.idTipoDocumento.Equals(documentTypeId));

            foreach (Document doc in docsPorTipo)
                docs.Add(doc);

            return docs;
        }

        public static IList<Document> GetList(int processId, int documentTypeId, bool active)
        {
            IList<Document> docs = null;

            Process pr = BizProcess.GetSingle(processId);

            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            IList<Document> docsPorTipo = ec.GetList(c => c.idTipoDocumento.Equals(documentTypeId))
                .Where(c => c.activo.Equals(active)).ToList();            

            foreach (Document doc in docsPorTipo)
                docs.Add(doc);

            return docs;
        }

        public static Document GetSingle(int id)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static Document GetSingle(string name)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();
            return ec.GetSingle(c => c.nombre.Equals(name));
        }

        public static void Add(params Document[] documents)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            ec.Add(documents);
        }

        public static void Update(params Document[] documents)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();

            ec.Update(documents);
        }

        public static void Remove(params Document[] documents)
        {
            EntityCRUD<Document> ec = new EntityCRUD<Document>();
            ec.Remove(documents);
        }

        public void Enable(params Document[] documents)
        {
            foreach (Document item in documents)
            {
                Document document = GetSingle(item.id);
                document.activo = true;

                Update(document);
            }
        }

        public void Disable(params Document[] documents)
        {
            foreach (Document item in documents)
            {
                Document document = GetSingle(item.id);
                document.activo = false;

                Update(document);
            }
        }        
    }
}
