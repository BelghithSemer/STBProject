using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.models
{
    public class CreditCards
    {
        [Key]
        public int Id { get; set; }
        public string Statut { get; set; }
        public string TypeDeCarte { get; set; }
        public string Libelle { get; set; }
        public decimal Solde { get; set; }
        public string AccountNumber { get; set; }
        public decimal Plafond { get; set; }

    }
}