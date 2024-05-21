using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.models;
using System;
using System.Threading.Tasks;

namespace projet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly GestionCreditContext _context;

        public TransactionController(GestionCreditContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string accountNumber)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountNumber == accountNumber)
                .ToListAsync();

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("Aucune transaction trouvée pour ce numéro de compte.");
            }

            return Ok(transactions);
        }



    }
}


