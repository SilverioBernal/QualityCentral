using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizProcess
    {
        public static IList<Process> GetList()
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetAll();
        }

        public static IList<Process> GetList(bool active)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetList(c => c.activo.Equals(active));
        }

        public static IList<Process> GetList(int customerId)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetList(c => c.idCliente.Equals(customerId));
        }

        public static IList<Process> GetList(int customerId, bool active)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetList(c => c.idCliente.Equals(customerId)).Where(c => c.activo.Equals(active)).ToList();
        }

        public static IList<Process> GetList(int customerId, int parentProcess)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetList(c => c.idProcesoPadre.Equals(customerId)).Where(c => c.idCliente.Equals(customerId)).ToList();
        }

        public static IList<Process> GetList(int customerId, int parentProcess, bool active)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetList(c => c.idProcesoPadre.Equals(customerId))
                .Where(c => c.idCliente.Equals(customerId) && c.activo.Equals(active)).ToList();
        }

        public static Process GetSingle(int id)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static Process GetSingle(string name)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            return ec.GetSingle(c => c.nombre.Equals(name));
        }        

        public static void Add(params Process[] processes)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();

            ec.Add(processes);
        }

        public static void Update(params Process[] processes)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();

            ec.Update(processes);
        }

        public static void Remove(params Process[] processes)
        {
            EntityCRUD<Process> ec = new EntityCRUD<Process>();
            ec.Remove(processes);
        }

        public void Enable(params Process[] processes)
        {
            foreach (Process item in processes)
            {
                Process process = GetSingle(item.id);
                process.activo = true;

                Update(process);
            }
        }

        public void Disable(params Process[] processes)
        {
            foreach (Process item in processes)
            {
                Process process = GetSingle(item.id);
                process.activo = false;

                Update(process);
            }
        }       
    }
}
