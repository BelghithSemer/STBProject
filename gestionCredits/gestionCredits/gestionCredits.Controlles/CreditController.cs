using gestionCredits.data;
using Microsoft.AspNetCore.Mvc;
using projet.models;
using System.Linq;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly GestionCreditContext _context;

        public CreditController(GestionCreditContext context)
        {
            _context = context;
        }

        [HttpGet("{accountNumber}")]
        public IActionResult GetCredits(string accountNumber)
        {
            var account = _context.Credits.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}
