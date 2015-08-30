using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrkIdea.QC.DAL
{
    public interface IEntityCRUD<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        //Customer GetByIntField(int intValue);
        //Customer GetByTextField(string textValue);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
    }
}
