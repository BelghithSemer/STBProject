using System.ComponentModel.DataAnnotations;

namespace projet.models
{
    public class Sold
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string AccountName { get; set; }

    }
}
