using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using projet.models;
using System.Linq;

namespace projet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SoldController : Controller
    {
        private readonly GestionCreditContext _context;

        public SoldController(GestionCreditContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBalances([FromQuery] string accountNumber)
        {
            var solds = _context.Solds.Where(s => s.AccountNumber == accountNumber).ToList();
            if (!solds.Any())
            {
                return NotFound("Compte introuvable");
            }

            var totalBalance = solds.Sum(s => s.Balance);
            var viewModel = new SoldViewModel
            {
                Solds = solds,
                TotalBalance = totalBalance
            };
            return View(viewModel);
        }
    }
}
