/*using Microsoft.AspNetCore.Http;*/

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet.models
{
    public class CreditApplication
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentResidentialAddress { get; set; } 
        public string PhoneNumber { get; set; }
        public decimal salarity { get; set; }
        public string MaritalStatus { get; set; }
        public string job { get; set; }
        public decimal seniority { get; set; }
        public string contract { get; set; }
        public decimal otherIncomes { get; set; }
        public decimal repaymentYrears { get; set; }
        public decimal LoanAmountRequested { get; set; }
        public string LoanPurpose { get; set; }
        public string AccountNumber { get; set; }

        [NotMapped]
        public IFormFileCollection? files { get; set; }


        public List<string> FilePaths { get; set; } = new List<string>();
        public int userid  { get; set; }

        public string Status { get; set; }





    }

}
