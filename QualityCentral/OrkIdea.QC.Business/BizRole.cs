using OrkIdea.QC.DAL;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.Business
{
    public class BizRole
    {
        public static IList<Role> GetList()
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            return ec.GetAll();
        }

        public static IList<Role> GetList(int customerId)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            return ec.GetAll(c => c.idCliente.Equals(customerId));
        }

        public static IList<Role> GetList(int customerId, bool active)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            return ec.GetList(c => c.idCliente.Equals(customerId) && c.activo.Equals(active));
        }

        public static Role GetSingle(int id)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            return ec.GetSingle(c => c.id.Equals(id));
        }

        public static Role GetSingle(int customerId, string name)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            return ec.GetSingle(c => c.idCliente.Equals(customerId) && c.nombre.Equals(name));
        }

        public static void Add(params Role[] roles)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();

            ec.Add(roles);
        }

        public static void Update(params Role[] roles)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();

            ec.Update(roles);
        }

        public static void Remove(params Role[] roles)
        {
            EntityCRUD<Role> ec = new EntityCRUD<Role>();
            ec.Remove(roles);
        }

        public void Enable(params Role[] roles)
        {
            foreach (Role item in roles)
            {
                Role role = GetSingle(item.id);
                role.activo = true;

                Update(role);
            }
        }

        public void Disable(params Role[] roles)
        {
            foreach (Role item in roles)
            {
                Role role = GetSingle(item.id);
                role.activo = false;

                Update(role);
            }
        }        
    }
}
