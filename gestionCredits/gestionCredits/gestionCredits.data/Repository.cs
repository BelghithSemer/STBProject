using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        GestionCreditContext context;
        public Repository(GestionCreditContext context)//injection de dependance
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Add(entity);
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
        }
        public T Get(object id)
        {
            return context.Find<T>(id);
        }
        public IList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
             context.Update(entity);
        }


    }
}
