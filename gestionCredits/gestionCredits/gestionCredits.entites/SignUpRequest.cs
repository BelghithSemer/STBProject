using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.entites
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public decimal CIN { get; set; }
        public string PhoneNumber { get; set; }
        public string agency { get; set; }
        public string adsress { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string AccountNumber { get; set; }
        public string password { get; set; }
    

    }
}
