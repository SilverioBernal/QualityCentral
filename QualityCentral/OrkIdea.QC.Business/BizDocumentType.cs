
using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public static class BizDocumentType
    {
        public static IList<DocumentType> GetList()
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetAll();
        }

        public static IList<DocumentType> GetList(int customerId)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetAll(c => c.idCliente.Equals(customerId));
        }

        public static IList<DocumentType> GetList(int customerId, bool active)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetAll(c => c.idCliente.Equals(customerId)).Where(d => d.activo.Equals(active)).ToList();
        }

        public static IList<DocumentType> GetList(int customerId, int processId)
        {
            List<int> dts = BizDocument.GetList(processId).Distinct().Select(z => z.idTipoDocumento).ToList();
            
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetAll(c => c.idCliente.Equals(customerId)).Where(x => dts.Contains(x.id)).ToList();
        }

        public static DocumentType GetSingle(int id)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static DocumentType GetSingle(string name)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            return ec.GetSingle(c => c.nombre.Equals(name));
        }

        public static void Add(params DocumentType[] documentTypes)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();

            ec.Add(documentTypes);
        }

        public static void Update(params DocumentType[] documentTypes)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();

            ec.Update(documentTypes);
        }

        public static void Remove(params DocumentType[] documentTypes)
        {
            EntityCRUD<DocumentType> ec = new EntityCRUD<DocumentType>();
            ec.Remove(documentTypes);
        }

        public static void Enable(params DocumentType[] documentTypes)
        {
            foreach (DocumentType item in documentTypes)
            {
                DocumentType documentType = GetSingle(item.id);
                documentType.activo = true;

                Update(documentType);
            }
        }

        public static void Disable(params DocumentType[] documentTypes)
        {
            foreach (DocumentType item in documentTypes)
            {
                DocumentType documentType = GetSingle(item.id);
                documentType.activo = false;

                Update(documentType);
            }
        }        
    }
}
