using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Entities
{
    public class UserMenu
    {
        public int id { get; set; }
        public Nullable<int> idPadre { get; set; }
        public string tipo { get; set; }
        public string etiqueta { get; set; }
        public string url { get; set; }
        public bool publico { get; set; }
        public bool activo { get; set; }

        public List<UserMenu> lsHijos { get; set; }

        public UserMenu()
        {
            lsHijos = new List<UserMenu>();
        }
    }
}
