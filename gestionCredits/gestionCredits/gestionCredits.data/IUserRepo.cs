using projet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data
{
    public interface IUserRepo
    {
        User Add(User user);
        User GetByEmail(string email);
        void Update(User user);
        void Delete(User user);
        IList<User> GetAll();
    }
}
