using System.ComponentModel.DataAnnotations;

namespace projet.models
{
    public class CreditRequest
    {
        [Key]
        public int Id { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int CreditDuration { get; set; }
        public string? Periodicity { get; set; }
    }

    public class AmortizationPayment
    {
        [Key]
        public int Id { get; set; }
        public int PaymentNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class AmortizationResult
    {
        [Key]
        public int Id { get; set; }
        public List<AmortizationPayment>? Payments { get; set; }
    }
}