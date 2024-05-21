using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data
{
    public class UnitOfWork : IUnitOfWork
    {
        private GestionCreditContext context;
        public UnitOfWork(GestionCreditContext context)
        {
            this.context = context;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(context);
        }

        public IUserRepo GetUserRepository()
        {
            return new UserRepo(context);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
