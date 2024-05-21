using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using projet.models;

namespace VotreProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditSimulatorController : ControllerBase
    {
        [HttpPost("calculateAmortization")]
        public IActionResult CalculateAmortization([FromBody] CreditRequest request)
        {
          
            decimal monthlyInterestRate = (decimal)(request.InterestRate / 100) / 12;
            int numberOfPayments = request.CreditDuration * 12;

            double doubleMonthlyInterestRate = (double)monthlyInterestRate;
            double doubleNumberOfPayments = (double)numberOfPayments;

            decimal monthlyPayment = request.CreditAmount * monthlyInterestRate *
                (decimal)(Math.Pow(1 + doubleMonthlyInterestRate, doubleNumberOfPayments)) /
                (decimal)(Math.Pow(1 + doubleMonthlyInterestRate, doubleNumberOfPayments) - 1);

            // Calculer l'amortissement
            var result = new AmortizationResult
            {
                Payments = []
            };

            decimal outstandingBalance = request.CreditAmount;
            for (int i = 1; i <= numberOfPayments; i++)
            {
                decimal interestPayment = outstandingBalance * monthlyInterestRate;
                decimal principalPayment = monthlyPayment - interestPayment;
                outstandingBalance -= principalPayment;

                var payment = new AmortizationPayment
                {
                    PaymentNumber = i,
                    Date = DateTime.Now.AddMonths(i - 1), 
                    PrincipalPaid = principalPayment,
                    InterestPaid = interestPayment,
                    RemainingBalance = outstandingBalance
                };
                result.Payments.Add(payment);
            }
            return Ok(result);
        }
    }

 
}
