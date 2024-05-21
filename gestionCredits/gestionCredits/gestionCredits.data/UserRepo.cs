using projet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data
{
    public class UserRepo : IUserRepo
    {
        GestionCreditContext context;

        public UserRepo(GestionCreditContext context)//injection de dependance
        {
            this.context = context;
        }
        public User Add(User user)
        {
            context.Add(user);
            context.SaveChanges();
            return user;
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.email == email);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
