//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrkIdea.QC.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Process
    {
        public Process()
        {
            this.Document = new HashSet<Document>();
        }
    
        public int id { get; set; }
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public Nullable<int> idProcesoPadre { get; set; }
        public int idResponsable { get; set; }
        public bool activo { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Document> Document { get; set; }
    }
}
