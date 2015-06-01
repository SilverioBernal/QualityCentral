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
    public static class CRUDProcess
    {
        /*CRUD Process*/

        public static List<Process> GetProcessList()
        {

            List<Process> lstProcess = new List<Process>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstProcess = ctx.Process.ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstProcess;
        }

        public static List<Process> GetProcessList(int customerId)
        {

            List<Process> lstProcess = new List<Process>();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lstProcess = ctx.Process.Where(x => x.idCliente.Equals(customerId)).ToList();
                }
            }
            catch (Exception ex) { throw ex; }

            return lstProcess;
        }

        public static Process GetProcessByKey(int processId)
        {
            Process oProcess = new Process();

            try
            {
                using (var ctx = new QCEntities())
                {
                    ctx.Configuration.ProxyCreationEnabled = false;

                    oProcess =
                        ctx.Process.Where(x => x.id.Equals(processId)).FirstOrDefault();
                }
            }
            catch (Exception ex) { throw ex; }

            return oProcess;
        }

        public static void SaveProcess(Process process)
        {

            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the student exists
                    Process oProcess = GetProcessByKey(process.id);

                    if (oProcess != null)
                    {
                        // if exists then edit 
                        ctx.Process.Attach(oProcess);
                        EntityFrameworkHelper.EnumeratePropertyDifferences(oProcess, process);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // else create
                        ctx.Process.Add(process);
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

        public static void DeleteProcess(int processId)
        {
            try
            {
                using (var ctx = new QCEntities())
                {
                    //verify if the school exists
                    Process oProcess = GetProcessByKey(processId);

                    if (oProcess != null)
                    {
                        // if exists then edit 
                        ctx.Process.Attach(oProcess);
                        ctx.Process.Remove(oProcess);
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
