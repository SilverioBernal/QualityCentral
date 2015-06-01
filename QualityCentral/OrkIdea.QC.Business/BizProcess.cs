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
        public List<Process> GetProcesses()
        {
            return CRUDProcess.GetProcessList();
        }

        public List<Process> GetProcesses(int customerId)
        {
            return CRUDProcess.GetProcessList(customerId);
        }

        public List<Process> GetProcesses(int customerId, bool active)
        {
            return CRUDProcess.GetProcessList(customerId).Where(x => x.activo.Equals(active)).ToList();
        }

        public List<Process> GetChildProcesses(int parentProcessId)
        {
            Process process = GetProcess(parentProcessId);

            List<Process> customerProcesses = GetProcesses(process.idCliente);

            return  customerProcesses.Where(x=> x.idProcesoPadre.Equals(parentProcessId)).ToList();
        }

        public List<Process> GetChildProcesses(int parentProcessId, bool active)
        {
            Process process = GetProcess(parentProcessId);

            List<Process> customerProcesses = GetProcesses(process.idCliente);

            return customerProcesses.Where(x => x.idProcesoPadre.Equals(parentProcessId) && x.activo.Equals(active)).ToList();
        }

        public Process GetProcess(int processId)
        {
            return CRUDProcess.GetProcessByKey(processId);
        }

        public void SaveProcess(Process process)
        {
            CRUDProcess.SaveProcess(process);
        }

        public void EnableProcess(int processId)
        {
            Process process = GetProcess(processId);

            process.activo = true;

            SaveProcess(process);

        }

        public void DisableProcess(int processId)
        {
            BizDocument bizDocument = new BizDocument();
            Process process = GetProcess(processId);

            List<Document> processDocuments = bizDocument.GetDocumentsByProcess(processId, true);

            foreach (Document item in processDocuments)            
                bizDocument.DisableDocument(item.id);            

            process.activo = false;

            SaveProcess(process);
        }

        public void DeleteProcess(int processId)
        {
            BizDocument bizDocument = new BizDocument();
            Process process = GetProcess(processId);

            List<Document> processDocuments = bizDocument.GetDocumentsByProcess(processId, true);

            foreach (Document item in processDocuments)
            {
                item.activo = false;
                item.Process = null;

                bizDocument.SaveDocument(item);
            }
                        
            CRUDProcess.DeleteProcess(processId);
        }
    }
}
