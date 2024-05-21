using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T Get(object id);
        void Update(T entity);
        void Delete(T entity);
        IList<T> GetAll();
       

    }
}
