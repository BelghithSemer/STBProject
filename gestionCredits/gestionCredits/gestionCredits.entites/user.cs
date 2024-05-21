using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string IdRef { get; set; }
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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }






    }
}

