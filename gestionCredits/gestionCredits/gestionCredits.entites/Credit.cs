using System.ComponentModel.DataAnnotations;

namespace projet.models
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal TotalIssued { get; set; }
        public decimal TotalRedeemed { get; set; }
        public decimal Balance { get; set; }
        public int NumberOfMonthsToRepay { get; set; } 
        public decimal TotalDue { get; set; }
        public decimal capacity { get; set; }


    }
}
