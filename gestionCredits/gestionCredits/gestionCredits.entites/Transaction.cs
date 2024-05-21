using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace projet.models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; } = null!;
        public string Beneficiary { get; set; } = null!;
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; } = null!;
    }

}
